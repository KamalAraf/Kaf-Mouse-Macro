namespace KafMouseMacro;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    private ListBox lstPositions;
    private NumericUpDown nudDelay;
    private Button btnAdd;
    private Button btnRemove;
    private Button btnClear;
    private Button btnStart;
    private Button btnStop;
    private Button btnSave;
    private Button btnLoad;
    private Label lblCount;
    private Label lblStatus;
    private Label lblClickType;
    private ComboBox cmbClickType;
    private GroupBox grpControls;
    private TableLayoutPanel tlpRight;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        lstPositions = new ListBox();
        nudDelay = new NumericUpDown();
        btnAdd = new Button();
        btnRemove = new Button();
        btnClear = new Button();
        btnStart = new Button();
        btnStop = new Button();
        btnSave = new Button();
        btnLoad = new Button();
        lblCount = new Label();
        lblStatus = new Label();
        grpControls = new GroupBox();
        tlpRight = new TableLayoutPanel();

        ((System.ComponentModel.ISupportInitialize)nudDelay).BeginInit();
        grpControls.SuspendLayout();
        tlpRight.SuspendLayout();
        SuspendLayout();

        // lstPositions
        lstPositions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lstPositions.FormattingEnabled = true;
        lstPositions.ItemHeight = 15;
        lstPositions.Location = new Point(12, 28);
        lstPositions.Name = "lstPositions";
        lstPositions.Size = new Size(384, 358);
        lstPositions.TabIndex = 0;
        lstPositions.SelectedIndexChanged += lstPositions_SelectedIndexChanged;
        lstPositions.DoubleClick += (_, _) => RemoveSelected();

        // lblCount
        lblCount.AutoSize = true;
        lblCount.Location = new Point(12, 9);
        lblCount.Name = "lblCount";
        lblCount.Size = new Size(156, 15);
        lblCount.TabIndex = 10;
        lblCount.Text = "Nessuna posizione registrata.";

        // nudDelay
        nudDelay.Location = new Point(3, 23);
        nudDelay.Maximum = 60000;
        nudDelay.Minimum = 50;
        nudDelay.Name = "nudDelay";
        nudDelay.Size = new Size(100, 23);
        nudDelay.TabIndex = 1;
        nudDelay.Value = 1000;
        nudDelay.Increment = 50;
        nudDelay.ThousandsSeparator = true;

        // lblDelay
        var lblDelay = new Label();
        lblDelay.AutoSize = true;
        lblDelay.Location = new Point(3, 5);
        lblDelay.Name = "lblDelay";
        lblDelay.Size = new Size(56, 15);
        lblDelay.TabIndex = 9;
        lblDelay.Text = "Delay (ms):";

        // lblClickType
        lblClickType = new Label();
        lblClickType.AutoSize = true;
        lblClickType.Location = new Point(3, 50);
        lblClickType.Name = "lblClickType";
        lblClickType.Size = new Size(60, 15);
        lblClickType.TabIndex = 10;
        lblClickType.Text = "Tasto click:";

        // cmbClickType
        cmbClickType = new ComboBox();
        cmbClickType.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbClickType.Items.AddRange(new object[] { "Sinistro", "Destro" });
        cmbClickType.Location = new Point(3, 68);
        cmbClickType.Name = "cmbClickType";
        cmbClickType.Size = new Size(110, 23);
        cmbClickType.TabIndex = 2;
        cmbClickType.SelectedIndex = 0;

        // btnAdd
        btnAdd.AutoSize = true;
        btnAdd.Location = new Point(3, 95);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(168, 27);
        btnAdd.TabIndex = 3;
        btnAdd.Text = "Aggiungi posizione (Z)";
        btnAdd.UseVisualStyleBackColor = true;
        btnAdd.Click += (_, _) => AddCurrentPosition();

        // btnRemove
        btnRemove.AutoSize = true;
        btnRemove.Location = new Point(3, 128);
        btnRemove.Name = "btnRemove";
        btnRemove.Size = new Size(168, 27);
        btnRemove.TabIndex = 4;
        btnRemove.Text = "Rimuovi selezionato";
        btnRemove.UseVisualStyleBackColor = true;
        btnRemove.Click += (_, _) => RemoveSelected();

        // btnClear
        btnClear.AutoSize = true;
        btnClear.Location = new Point(3, 161);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(168, 27);
        btnClear.TabIndex = 5;
        btnClear.Text = "Pulisci tutto";
        btnClear.UseVisualStyleBackColor = true;
        btnClear.Click += (_, _) => ClearAll();

        // btnStart
        btnStart.AutoSize = true;
        btnStart.Location = new Point(3, 194);
        btnStart.Name = "btnStart";
        btnStart.Size = new Size(168, 27);
        btnStart.TabIndex = 6;
        btnStart.Text = "Avvia macro (X)";
        btnStart.UseVisualStyleBackColor = true;
        btnStart.Click += (_, _) => StartMacro();

        // btnStop
        btnStop.AutoSize = true;
        btnStop.Enabled = false;
        btnStop.Location = new Point(3, 227);
        btnStop.Name = "btnStop";
        btnStop.Size = new Size(168, 27);
        btnStop.TabIndex = 7;
        btnStop.Text = "Ferma macro (Q)";
        btnStop.UseVisualStyleBackColor = true;
        btnStop.Click += (_, _) => StopMacro();

        // btnSave
        btnSave.AutoSize = true;
        btnSave.Location = new Point(3, 260);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(168, 27);
        btnSave.TabIndex = 8;
        btnSave.Text = "Salva...";
        btnSave.UseVisualStyleBackColor = true;
        btnSave.Click += (_, _) => SaveToFile();

        // btnLoad
        btnLoad.AutoSize = true;
        btnLoad.Location = new Point(3, 293);
        btnLoad.Name = "btnLoad";
        btnLoad.Size = new Size(168, 27);
        btnLoad.TabIndex = 9;
        btnLoad.Text = "Carica...";
        btnLoad.UseVisualStyleBackColor = true;
        btnLoad.Click += (_, _) => LoadFromFile();

        // tlpRight
        tlpRight.ColumnCount = 1;
        tlpRight.RowCount = 11;
        tlpRight.Location = new Point(6, 22);
        tlpRight.Name = "tlpRight";
        tlpRight.Size = new Size(220, 340);
        tlpRight.TabIndex = 10;
        tlpRight.Controls.Add(lblDelay, 0, 0);
        tlpRight.Controls.Add(nudDelay, 0, 1);
        tlpRight.Controls.Add(lblClickType, 0, 2);
        tlpRight.Controls.Add(cmbClickType, 0, 3);
        tlpRight.Controls.Add(btnAdd, 0, 4);
        tlpRight.Controls.Add(btnRemove, 0, 5);
        tlpRight.Controls.Add(btnClear, 0, 6);
        tlpRight.Controls.Add(btnStart, 0, 7);
        tlpRight.Controls.Add(btnStop, 0, 8);
        tlpRight.Controls.Add(btnSave, 0, 9);
        tlpRight.Controls.Add(btnLoad, 0, 10);

        // grpControls
        grpControls.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
        grpControls.Controls.Add(tlpRight);
        grpControls.Location = new Point(402, 9);
        grpControls.Name = "grpControls";
        grpControls.Size = new Size(226, 380);
        grpControls.TabIndex = 11;
        grpControls.TabStop = false;
        grpControls.Text = "Controlli";

        // lblStatus
        lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblStatus.AutoSize = true;
        lblStatus.Location = new Point(12, 405);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(41, 15);
        lblStatus.TabIndex = 12;
        lblStatus.Text = "Pronto";

        // MainForm
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(640, 432);
        Controls.Add(lblStatus);
        Controls.Add(grpControls);
        Controls.Add(lblCount);
        Controls.Add(lstPositions);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimumSize = new Size(656, 471);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Kaf Mouse Macro";

        ((System.ComponentModel.ISupportInitialize)nudDelay).EndInit();
        grpControls.ResumeLayout(false);
        tlpRight.ResumeLayout(false);
        tlpRight.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    private void lstPositions_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnRemove.Enabled = !_isRunning && lstPositions.SelectedIndex >= 0;
    }
}
