using System.Text.Json;
using KafMouseMacro.Models;
using KafMouseMacro.Services;

namespace KafMouseMacro;

#pragma warning disable CS8618

public partial class MainForm : Form
{
    private const int HotkeyZ = 1;
    private const int HotkeyX = 2;
    private const int HotkeyQ = 3;

    private const uint VkZ = 0x5A;
    private const uint VkX = 0x58;
    private const uint VkQ = 0x51;

    private readonly List<MacroPoint> _points = new();
    private readonly MacroEngine _engine = new();
    private CancellationTokenSource? _cts;
    private bool _isRunning;
    private bool _hotkeysRegistered;

    public MainForm()
    {
        InitializeComponent();
        RegisterHotkeys();
        UpdateUIState();
    }

    private void RegisterHotkeys()
    {
        var handle = Handle;
        _hotkeysRegistered = true;

        if (!HotkeyManager.Register(handle, HotkeyZ, VkZ))
            ShowHotkeyWarning('Z');

        if (!HotkeyManager.Register(handle, HotkeyX, VkX))
            ShowHotkeyWarning('X');

        if (!HotkeyManager.Register(handle, HotkeyQ, VkQ))
            ShowHotkeyWarning('Q');
    }

    private static void ShowHotkeyWarning(char key)
    {
        MessageBox.Show(
            $"Impossibile registrare l'hotkey ({key}). Potrebbe essere gia in uso da un'altra applicazione.",
            "Kaf Mouse Macro",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
    }

    protected override void WndProc(ref Message m)
    {
        if (HotkeyManager.TryProcessMessage(ref m, out int hotkeyId))
        {
            switch (hotkeyId)
            {
                case HotkeyZ when !_isRunning:
                    AddCurrentPosition();
                    break;
                case HotkeyX when !_isRunning:
                    StartMacro();
                    break;
                case HotkeyQ when _isRunning:
                    StopMacro();
                    break;
            }
        }

        base.WndProc(ref m);
    }

    private void RefreshList()
    {
        lstPositions.Items.Clear();
        for (int i = 0; i < _points.Count; i++)
        {
            lstPositions.Items.Add($"{i + 1}: ({_points[i].X}, {_points[i].Y})");
        }

        lblCount.Text = _points.Count == 0
            ? "Nessuna posizione registrata."
            : $"Posizioni registrate: {_points.Count}";
    }

    private void AddCurrentPosition()
    {
        var pos = Cursor.Position;
        _points.Add(new MacroPoint(pos.X, pos.Y));
        RefreshList();
        lstPositions.TopIndex = _points.Count - 1;
    }

    private void RemoveSelected()
    {
        int index = lstPositions.SelectedIndex;
        if (index < 0 || index >= _points.Count)
            return;

        _points.RemoveAt(index);
        RefreshList();

        if (_points.Count > 0)
            lstPositions.SelectedIndex = Math.Min(index, _points.Count - 1);
    }

    private void ClearAll()
    {
        if (_points.Count == 0)
            return;

        var result = MessageBox.Show(
            "Rimuovere tutte le posizioni registrate?",
            "Kaf Mouse Macro",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            _points.Clear();
            RefreshList();
        }
    }

    private async void StartMacro()
    {
        if (_points.Count == 0)
        {
            MessageBox.Show(
                "Registrare almeno una posizione prima di avviare la macro.",
                "Kaf Mouse Macro",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        _cts = new CancellationTokenSource();
        _isRunning = true;
        UpdateUIState();

        try
        {
            await Task.Run(() =>
                _engine.StartAsync(_points.AsReadOnly(), (int)nudDelay.Value, _cts.Token));
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            _isRunning = false;
            _cts.Dispose();
            _cts = null;
            UpdateUIState();
        }
    }

    private void StopMacro()
    {
        _cts?.Cancel();
    }

    private void UpdateUIState()
    {
        btnAdd.Enabled = !_isRunning;
        btnRemove.Enabled = !_isRunning && _points.Count > 0;
        btnClear.Enabled = !_isRunning && _points.Count > 0;
        btnStart.Enabled = !_isRunning && _points.Count > 0;
        btnStop.Enabled = _isRunning;
        btnSave.Enabled = !_isRunning;
        btnLoad.Enabled = !_isRunning;
        nudDelay.Enabled = !_isRunning;

        lblStatus.Text = _isRunning
            ? "Macro in esecuzione..."
            : "Pronto";

        lblStatus.ForeColor = _isRunning ? Color.Green : SystemColors.ControlText;
    }

    private void SaveToFile()
    {
        using var dialog = new SaveFileDialog
        {
            Filter = "File JSON (*.json)|*.json|Tutti i file (*.*)|*.*",
            DefaultExt = "json",
            FileName = "posizioni.json",
            Title = "Salva posizioni"
        };

        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        try
        {
            var json = JsonSerializer.Serialize(_points, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(dialog.FileName, json);
            lblStatus.Text = $"Salvato: {Path.GetFileName(dialog.FileName)}";
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Errore durante il salvataggio:\n{ex.Message}",
                "Errore",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void LoadFromFile()
    {
        using var dialog = new OpenFileDialog
        {
            Filter = "File JSON (*.json)|*.json|Tutti i file (*.*)|*.*",
            DefaultExt = "json",
            Title = "Carica posizioni"
        };

        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        try
        {
            var json = File.ReadAllText(dialog.FileName);
            var loaded = JsonSerializer.Deserialize<List<MacroPoint>>(json);
            if (loaded is null || loaded.Count == 0)
            {
                MessageBox.Show(
                    "Il file non contiene posizioni valide.",
                    "Kaf Mouse Macro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            _points.Clear();
            _points.AddRange(loaded);
            RefreshList();
            lblStatus.Text = $"Caricato: {Path.GetFileName(dialog.FileName)}";
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Errore durante il caricamento:\n{ex.Message}",
                "Errore",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (_isRunning)
        {
            _cts?.Cancel();
        }

        if (_hotkeysRegistered)
        {
            HotkeyManager.UnregisterAll(Handle, HotkeyZ, HotkeyX, HotkeyQ);
            _hotkeysRegistered = false;
        }

        base.OnFormClosing(e);
    }
}
