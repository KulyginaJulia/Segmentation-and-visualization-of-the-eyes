namespace GlazSegment
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.glControl1 = new OpenTK.GLControl();
            this.glControl2 = new OpenTK.GLControl();
            this.currentlayer = new System.Windows.Forms.HScrollBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.левыйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.правыйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.статистикаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dИзображениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.информацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.rrright = new System.Windows.Forms.RadioButton();
            this.rrleft = new System.Windows.Forms.RadioButton();
            this.rotleft = new System.Windows.Forms.Button();
            this.rotright = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.PathRight = new System.Windows.Forms.TextBox();
            this.PathLeft = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button7 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.window_max = new System.Windows.Forms.TextBox();
            this.window_min = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button_to3D = new System.Windows.Forms.Button();
            this.radioButton_knife = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.wpOffset = new System.Windows.Forms.TextBox();
            this.rWandPen = new System.Windows.Forms.RadioButton();
            this.cSize = new System.Windows.Forms.ComboBox();
            this.cForm = new System.Windows.Forms.ComboBox();
            this.cColor = new System.Windows.Forms.ComboBox();
            this.rEraser = new System.Windows.Forms.RadioButton();
            this.rPen = new System.Windows.Forms.RadioButton();
            this.LoadMask = new System.Windows.Forms.Button();
            this.AddMask = new System.Windows.Forms.Button();
            this.MaskInfo = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button_repaint = new System.Windows.Forms.Button();
            this.radioButton_x_z = new System.Windows.Forms.RadioButton();
            this.radioButton_y_z = new System.Windows.Forms.RadioButton();
            this.radiobutton_x_y = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.volMax = new System.Windows.Forms.TextBox();
            this.volMin = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.voldp = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.isCurrent = new System.Windows.Forms.CheckBox();
            this.rbRight = new System.Windows.Forms.RadioButton();
            this.rbLeft = new System.Windows.Forms.RadioButton();
            this.groupbox = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.bMax = new System.Windows.Forms.Button();
            this.bMin = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.masks = new System.Windows.Forms.CheckedListBox();
            this.menuStrip1.SuspendLayout();
            this.Tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupbox.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Location = new System.Drawing.Point(12, 190);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(350, 350);
            this.glControl1.TabIndex = 10;
            this.glControl1.VSync = false;
            this.glControl1.Load += new System.EventHandler(this.glControl1_Load);
            this.glControl1.Click += new System.EventHandler(this.glControl1_Click);
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.glControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseMove);
            // 
            // glControl2
            // 
            this.glControl2.BackColor = System.Drawing.Color.Black;
            this.glControl2.Location = new System.Drawing.Point(368, 190);
            this.glControl2.Name = "glControl2";
            this.glControl2.Size = new System.Drawing.Size(350, 350);
            this.glControl2.TabIndex = 11;
            this.glControl2.VSync = false;
            this.glControl2.Load += new System.EventHandler(this.glControl2_Load);
            this.glControl2.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl2_Paint);
            this.glControl2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl2_MouseMove);
            // 
            // currentlayer
            // 
            this.currentlayer.Enabled = false;
            this.currentlayer.Location = new System.Drawing.Point(9, 543);
            this.currentlayer.Minimum = 1;
            this.currentlayer.Name = "currentlayer";
            this.currentlayer.Size = new System.Drawing.Size(709, 17);
            this.currentlayer.TabIndex = 12;
            this.currentlayer.Value = 1;
            this.currentlayer.Scroll += new System.Windows.Forms.ScrollEventHandler(this.currentlayer_Scroll);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.оПрограммеToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(958, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem,
            this.сохранитьToolStripMenuItem,
            this.dИзображениеToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.левыйToolStripMenuItem,
            this.правыйToolStripMenuItem});
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            // 
            // левыйToolStripMenuItem
            // 
            this.левыйToolStripMenuItem.Name = "левыйToolStripMenuItem";
            this.левыйToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.левыйToolStripMenuItem.Text = "Левый";
            this.левыйToolStripMenuItem.Click += new System.EventHandler(this.левыйToolStripMenuItem_Click);
            // 
            // правыйToolStripMenuItem
            // 
            this.правыйToolStripMenuItem.Name = "правыйToolStripMenuItem";
            this.правыйToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.правыйToolStripMenuItem.Text = "Правый";
            this.правыйToolStripMenuItem.Click += new System.EventHandler(this.правыйToolStripMenuItem_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.статистикаToolStripMenuItem});
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            // 
            // статистикаToolStripMenuItem
            // 
            this.статистикаToolStripMenuItem.Name = "статистикаToolStripMenuItem";
            this.статистикаToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.статистикаToolStripMenuItem.Text = "Статистика";
            this.статистикаToolStripMenuItem.Click += new System.EventHandler(this.статистикаToolStripMenuItem_Click);
            // 
            // dИзображениеToolStripMenuItem
            // 
            this.dИзображениеToolStripMenuItem.Name = "dИзображениеToolStripMenuItem";
            this.dИзображениеToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.dИзображениеToolStripMenuItem.Text = "3D изображение";
            this.dИзображениеToolStripMenuItem.Click += new System.EventHandler(this.dИзображениеToolStripMenuItem_Click);
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.информацияToolStripMenuItem});
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            // 
            // информацияToolStripMenuItem
            // 
            this.информацияToolStripMenuItem.Name = "информацияToolStripMenuItem";
            this.информацияToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.информацияToolStripMenuItem.Text = "Информация";
            this.информацияToolStripMenuItem.Click += new System.EventHandler(this.информацияToolStripMenuItem_Click);
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.tabPage1);
            this.Tabs.Controls.Add(this.tabPage2);
            this.Tabs.Controls.Add(this.tabPage3);
            this.Tabs.Enabled = false;
            this.Tabs.Location = new System.Drawing.Point(12, 27);
            this.Tabs.Multiline = true;
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(936, 157);
            this.Tabs.TabIndex = 17;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox7);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(928, 131);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Основные";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.rrright);
            this.groupBox7.Controls.Add(this.rrleft);
            this.groupBox7.Controls.Add(this.rotleft);
            this.groupBox7.Controls.Add(this.rotright);
            this.groupBox7.Location = new System.Drawing.Point(144, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(138, 122);
            this.groupBox7.TabIndex = 23;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Вращение";
            // 
            // rrright
            // 
            this.rrright.AutoSize = true;
            this.rrright.Checked = true;
            this.rrright.Location = new System.Drawing.Point(6, 99);
            this.rrright.Name = "rrright";
            this.rrright.Size = new System.Drawing.Size(59, 17);
            this.rrright.TabIndex = 3;
            this.rrright.TabStop = true;
            this.rrright.Text = "Левый";
            this.rrright.UseVisualStyleBackColor = true;
            this.rrright.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rrright_MouseClick);
            // 
            // rrleft
            // 
            this.rrleft.AutoSize = true;
            this.rrleft.Location = new System.Drawing.Point(72, 99);
            this.rrleft.Name = "rrleft";
            this.rrleft.Size = new System.Drawing.Size(65, 17);
            this.rrleft.TabIndex = 2;
            this.rrleft.Text = "Правый";
            this.rrleft.UseVisualStyleBackColor = true;
            this.rrleft.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rrleft_MouseClick);
            // 
            // rotleft
            // 
            this.rotleft.Enabled = false;
            this.rotleft.Location = new System.Drawing.Point(5, 45);
            this.rotleft.Name = "rotleft";
            this.rotleft.Size = new System.Drawing.Size(126, 23);
            this.rotleft.TabIndex = 1;
            this.rotleft.Text = "90° Вправо";
            this.rotleft.UseVisualStyleBackColor = true;
            this.rotleft.Click += new System.EventHandler(this.rotleft_Click);
            // 
            // rotright
            // 
            this.rotright.Enabled = false;
            this.rotright.Location = new System.Drawing.Point(5, 19);
            this.rotright.Name = "rotright";
            this.rotright.Size = new System.Drawing.Size(126, 23);
            this.rotright.TabIndex = 0;
            this.rotright.Text = "90° Влево";
            this.rotright.UseVisualStyleBackColor = true;
            this.rotright.Click += new System.EventHandler(this.rotright_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.progressBar1);
            this.groupBox2.Controls.Add(this.PathRight);
            this.groupBox2.Controls.Add(this.PathLeft);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(288, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(634, 122);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Файлы";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(9, 89);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(617, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // PathRight
            // 
            this.PathRight.Location = new System.Drawing.Point(59, 48);
            this.PathRight.Name = "PathRight";
            this.PathRight.ReadOnly = true;
            this.PathRight.Size = new System.Drawing.Size(567, 20);
            this.PathRight.TabIndex = 3;
            // 
            // PathLeft
            // 
            this.PathLeft.Location = new System.Drawing.Point(59, 18);
            this.PathLeft.Name = "PathLeft";
            this.PathLeft.ReadOnly = true;
            this.PathLeft.Size = new System.Drawing.Size(567, 20);
            this.PathLeft.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Правый:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Левый: ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.window_max);
            this.groupBox1.Controls.Add(this.window_min);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(135, 122);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Плотности";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(9, 89);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(114, 27);
            this.button7.TabIndex = 25;
            this.button7.Text = "Задать";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Max:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Min:";
            // 
            // window_max
            // 
            this.window_max.Location = new System.Drawing.Point(39, 19);
            this.window_max.Name = "window_max";
            this.window_max.Size = new System.Drawing.Size(84, 20);
            this.window_max.TabIndex = 22;
            // 
            // window_min
            // 
            this.window_min.Location = new System.Drawing.Point(39, 49);
            this.window_min.Name = "window_min";
            this.window_min.Size = new System.Drawing.Size(84, 20);
            this.window_min.TabIndex = 21;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button_to3D);
            this.tabPage2.Controls.Add(this.radioButton_knife);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.wpOffset);
            this.tabPage2.Controls.Add(this.rWandPen);
            this.tabPage2.Controls.Add(this.cSize);
            this.tabPage2.Controls.Add(this.cForm);
            this.tabPage2.Controls.Add(this.cColor);
            this.tabPage2.Controls.Add(this.rEraser);
            this.tabPage2.Controls.Add(this.rPen);
            this.tabPage2.Controls.Add(this.LoadMask);
            this.tabPage2.Controls.Add(this.AddMask);
            this.tabPage2.Controls.Add(this.MaskInfo);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(928, 131);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Маски";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button_to3D
            // 
            this.button_to3D.Location = new System.Drawing.Point(341, 6);
            this.button_to3D.Name = "button_to3D";
            this.button_to3D.Size = new System.Drawing.Size(75, 103);
            this.button_to3D.TabIndex = 13;
            this.button_to3D.Text = "Вырезать криволинейным контуром";
            this.button_to3D.UseVisualStyleBackColor = true;
            this.button_to3D.Click += new System.EventHandler(this.button_to3D_Click);
            // 
            // radioButton_knife
            // 
            this.radioButton_knife.AutoSize = true;
            this.radioButton_knife.Location = new System.Drawing.Point(260, 35);
            this.radioButton_knife.Name = "radioButton_knife";
            this.radioButton_knife.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioButton_knife.Size = new System.Drawing.Size(59, 17);
            this.radioButton_knife.TabIndex = 12;
            this.radioButton_knife.TabStop = true;
            this.radioButton_knife.Text = "Ножик";
            this.radioButton_knife.UseVisualStyleBackColor = true;
            this.radioButton_knife.CheckedChanged += new System.EventHandler(this.radioButton_knife_CheckedChanged);
            this.radioButton_knife.MouseClick += new System.Windows.Forms.MouseEventHandler(this.radioButton_knife_MouseClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(260, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // wpOffset
            // 
            this.wpOffset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.wpOffset.Enabled = false;
            this.wpOffset.Location = new System.Drawing.Point(87, 89);
            this.wpOffset.Name = "wpOffset";
            this.wpOffset.Size = new System.Drawing.Size(121, 20);
            this.wpOffset.TabIndex = 9;
            this.wpOffset.Text = "8";
            // 
            // rWandPen
            // 
            this.rWandPen.AutoSize = true;
            this.rWandPen.Location = new System.Drawing.Point(0, 82);
            this.rWandPen.Name = "rWandPen";
            this.rWandPen.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rWandPen.Size = new System.Drawing.Size(82, 30);
            this.rWandPen.TabIndex = 8;
            this.rWandPen.Text = "Волшебная\r\nпалочка";
            this.rWandPen.UseVisualStyleBackColor = true;
            this.rWandPen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rWandPen_MouseClick);
            // 
            // cSize
            // 
            this.cSize.FormattingEnabled = true;
            this.cSize.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "10",
            "11",
            "12"});
            this.cSize.Location = new System.Drawing.Point(214, 33);
            this.cSize.Name = "cSize";
            this.cSize.Size = new System.Drawing.Size(38, 21);
            this.cSize.TabIndex = 7;
            // 
            // cForm
            // 
            this.cForm.FormattingEnabled = true;
            this.cForm.Items.AddRange(new object[] {
            "Квадрат",
            "Круг"});
            this.cForm.Location = new System.Drawing.Point(87, 33);
            this.cForm.Name = "cForm";
            this.cForm.Size = new System.Drawing.Size(121, 21);
            this.cForm.TabIndex = 6;
            // 
            // cColor
            // 
            this.cColor.FormattingEnabled = true;
            this.cColor.Items.AddRange(new object[] {
            "Красный",
            "Зеленый",
            "Синий",
            "Желтый"});
            this.cColor.Location = new System.Drawing.Point(87, 61);
            this.cColor.Name = "cColor";
            this.cColor.Size = new System.Drawing.Size(121, 21);
            this.cColor.TabIndex = 5;
            // 
            // rEraser
            // 
            this.rEraser.AutoSize = true;
            this.rEraser.Location = new System.Drawing.Point(20, 62);
            this.rEraser.Name = "rEraser";
            this.rEraser.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rEraser.Size = new System.Drawing.Size(62, 17);
            this.rEraser.TabIndex = 4;
            this.rEraser.Text = "Ластик";
            this.rEraser.UseVisualStyleBackColor = true;
            this.rEraser.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rEraser_MouseClick);
            // 
            // rPen
            // 
            this.rPen.AutoSize = true;
            this.rPen.Checked = true;
            this.rPen.Location = new System.Drawing.Point(26, 35);
            this.rPen.Name = "rPen";
            this.rPen.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rPen.Size = new System.Drawing.Size(55, 17);
            this.rPen.TabIndex = 3;
            this.rPen.TabStop = true;
            this.rPen.Text = "Кисть";
            this.rPen.UseVisualStyleBackColor = true;
            this.rPen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rPen_MouseClick);
            // 
            // LoadMask
            // 
            this.LoadMask.Location = new System.Drawing.Point(87, 6);
            this.LoadMask.Name = "LoadMask";
            this.LoadMask.Size = new System.Drawing.Size(75, 23);
            this.LoadMask.TabIndex = 2;
            this.LoadMask.Text = "Загрузить";
            this.LoadMask.UseVisualStyleBackColor = true;
            this.LoadMask.Click += new System.EventHandler(this.LoadMask_Click);
            // 
            // AddMask
            // 
            this.AddMask.Location = new System.Drawing.Point(6, 6);
            this.AddMask.Name = "AddMask";
            this.AddMask.Size = new System.Drawing.Size(75, 23);
            this.AddMask.TabIndex = 1;
            this.AddMask.Text = "Добавить";
            this.AddMask.UseVisualStyleBackColor = true;
            this.AddMask.Click += new System.EventHandler(this.AddMask_Click);
            // 
            // MaskInfo
            // 
            this.MaskInfo.Location = new System.Drawing.Point(168, 6);
            this.MaskInfo.Name = "MaskInfo";
            this.MaskInfo.Size = new System.Drawing.Size(86, 23);
            this.MaskInfo.TabIndex = 0;
            this.MaskInfo.Text = "Информация";
            this.MaskInfo.UseVisualStyleBackColor = true;
            this.MaskInfo.Click += new System.EventHandler(this.MaskInfo_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox5);
            this.tabPage3.Controls.Add(this.groupBox4);
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Controls.Add(this.groupbox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(928, 131);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Вывести";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button_repaint);
            this.groupBox5.Controls.Add(this.radioButton_x_z);
            this.groupBox5.Controls.Add(this.radioButton_y_z);
            this.groupBox5.Controls.Add(this.radiobutton_x_y);
            this.groupBox5.Location = new System.Drawing.Point(336, 7);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(113, 121);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Плоскости";
            // 
            // button_repaint
            // 
            this.button_repaint.Location = new System.Drawing.Point(6, 86);
            this.button_repaint.Name = "button_repaint";
            this.button_repaint.Size = new System.Drawing.Size(101, 23);
            this.button_repaint.TabIndex = 8;
            this.button_repaint.Text = "Перерисовать";
            this.button_repaint.UseVisualStyleBackColor = true;
            this.button_repaint.Click += new System.EventHandler(this.button_repaint_Click);
            // 
            // radioButton_x_z
            // 
            this.radioButton_x_z.AutoSize = true;
            this.radioButton_x_z.Location = new System.Drawing.Point(6, 62);
            this.radioButton_x_z.Name = "radioButton_x_z";
            this.radioButton_x_z.Size = new System.Drawing.Size(51, 17);
            this.radioButton_x_z.TabIndex = 7;
            this.radioButton_x_z.TabStop = true;
            this.radioButton_x_z.Text = "(X, Z)";
            this.radioButton_x_z.UseVisualStyleBackColor = true;
            // 
            // radioButton_y_z
            // 
            this.radioButton_y_z.AutoSize = true;
            this.radioButton_y_z.Cursor = System.Windows.Forms.Cursors.Default;
            this.radioButton_y_z.Location = new System.Drawing.Point(6, 40);
            this.radioButton_y_z.Name = "radioButton_y_z";
            this.radioButton_y_z.Size = new System.Drawing.Size(51, 17);
            this.radioButton_y_z.TabIndex = 6;
            this.radioButton_y_z.Text = "(Y, Z)";
            this.radioButton_y_z.UseVisualStyleBackColor = true;
            // 
            // radiobutton_x_y
            // 
            this.radiobutton_x_y.AutoSize = true;
            this.radiobutton_x_y.Checked = true;
            this.radiobutton_x_y.Location = new System.Drawing.Point(6, 18);
            this.radiobutton_x_y.Name = "radiobutton_x_y";
            this.radiobutton_x_y.Size = new System.Drawing.Size(51, 17);
            this.radiobutton_x_y.TabIndex = 5;
            this.radiobutton_x_y.TabStop = true;
            this.radiobutton_x_y.Text = "(X, Y)";
            this.radiobutton_x_y.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.volMax);
            this.groupBox4.Controls.Add(this.volMin);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.voldp);
            this.groupBox4.Location = new System.Drawing.Point(217, 7);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(113, 121);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Объемы";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Max:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Min:";
            // 
            // volMax
            // 
            this.volMax.Location = new System.Drawing.Point(39, 17);
            this.volMax.Name = "volMax";
            this.volMax.Size = new System.Drawing.Size(62, 20);
            this.volMax.TabIndex = 33;
            this.volMax.Text = "1";
            // 
            // volMin
            // 
            this.volMin.Location = new System.Drawing.Point(39, 40);
            this.volMin.Name = "volMin";
            this.volMin.Size = new System.Drawing.Size(65, 20);
            this.volMin.TabIndex = 29;
            this.volMin.Text = "0";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(5, 88);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "По Маске";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // voldp
            // 
            this.voldp.Location = new System.Drawing.Point(5, 63);
            this.voldp.Name = "voldp";
            this.voldp.Size = new System.Drawing.Size(99, 23);
            this.voldp.TabIndex = 4;
            this.voldp.Text = "По диапазону";
            this.voldp.UseVisualStyleBackColor = true;
            this.voldp.Click += new System.EventHandler(this.voldp_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.isCurrent);
            this.groupBox3.Controls.Add(this.rbRight);
            this.groupBox3.Controls.Add(this.rbLeft);
            this.groupBox3.Location = new System.Drawing.Point(3, 7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(113, 121);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Общее";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 86);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(86, 17);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Округление";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // isCurrent
            // 
            this.isCurrent.AutoSize = true;
            this.isCurrent.Location = new System.Drawing.Point(6, 63);
            this.isCurrent.Name = "isCurrent";
            this.isCurrent.Size = new System.Drawing.Size(98, 17);
            this.isCurrent.TabIndex = 7;
            this.isCurrent.Text = "Текущий слой";
            this.isCurrent.UseVisualStyleBackColor = true;
            // 
            // rbRight
            // 
            this.rbRight.AutoSize = true;
            this.rbRight.Cursor = System.Windows.Forms.Cursors.Default;
            this.rbRight.Location = new System.Drawing.Point(6, 40);
            this.rbRight.Name = "rbRight";
            this.rbRight.Size = new System.Drawing.Size(65, 17);
            this.rbRight.TabIndex = 6;
            this.rbRight.Text = "Правый";
            this.rbRight.UseVisualStyleBackColor = true;
            // 
            // rbLeft
            // 
            this.rbLeft.AutoSize = true;
            this.rbLeft.Checked = true;
            this.rbLeft.Location = new System.Drawing.Point(6, 18);
            this.rbLeft.Name = "rbLeft";
            this.rbLeft.Size = new System.Drawing.Size(59, 17);
            this.rbLeft.TabIndex = 5;
            this.rbLeft.TabStop = true;
            this.rbLeft.Text = "Левый";
            this.rbLeft.UseVisualStyleBackColor = true;
            // 
            // groupbox
            // 
            this.groupbox.Controls.Add(this.button4);
            this.groupbox.Controls.Add(this.bMax);
            this.groupbox.Controls.Add(this.bMin);
            this.groupbox.Location = new System.Drawing.Point(122, 7);
            this.groupbox.Name = "groupbox";
            this.groupbox.Size = new System.Drawing.Size(89, 121);
            this.groupbox.TabIndex = 5;
            this.groupbox.TabStop = false;
            this.groupbox.Text = "Плотности";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(6, 63);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "По маске";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // bMax
            // 
            this.bMax.Location = new System.Drawing.Point(6, 38);
            this.bMax.Name = "bMax";
            this.bMax.Size = new System.Drawing.Size(75, 23);
            this.bMax.TabIndex = 3;
            this.bMax.Text = "Максимум";
            this.bMax.UseVisualStyleBackColor = true;
            this.bMax.Click += new System.EventHandler(this.bMax_Click);
            // 
            // bMin
            // 
            this.bMin.Location = new System.Drawing.Point(6, 14);
            this.bMin.Name = "bMin";
            this.bMin.Size = new System.Drawing.Size(75, 23);
            this.bMin.TabIndex = 2;
            this.bMin.Text = "Минимум";
            this.bMin.UseVisualStyleBackColor = true;
            this.bMin.Click += new System.EventHandler(this.bMin_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(721, 190);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(225, 372);
            this.tabControl1.TabIndex = 18;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.masks);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(217, 346);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Список масок";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // masks
            // 
            this.masks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.masks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.masks.FormattingEnabled = true;
            this.masks.Location = new System.Drawing.Point(0, 0);
            this.masks.Margin = new System.Windows.Forms.Padding(5);
            this.masks.Name = "masks";
            this.masks.Size = new System.Drawing.Size(217, 330);
            this.masks.TabIndex = 15;
            this.masks.SelectedIndexChanged += new System.EventHandler(this.masks_SelectedIndexChanged_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 569);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.Tabs);
            this.Controls.Add(this.currentlayer);
            this.Controls.Add(this.glControl2);
            this.Controls.Add(this.glControl1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "EyeAssistant";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.Tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupbox.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl glControl1;
        private OpenTK.GLControl glControl2;
        private System.Windows.Forms.HScrollBar currentlayer;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem левыйToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem правыйToolStripMenuItem;
        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button MaskInfo;
        private System.Windows.Forms.Button AddMask;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox window_max;
        private System.Windows.Forms.TextBox window_min;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox volMax;
        private System.Windows.Forms.TextBox volMin;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button voldp;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox isCurrent;
        private System.Windows.Forms.RadioButton rbRight;
        private System.Windows.Forms.RadioButton rbLeft;
        private System.Windows.Forms.GroupBox groupbox;
        private System.Windows.Forms.Button bMax;
        private System.Windows.Forms.Button bMin;
        private System.Windows.Forms.Button LoadMask;
        private System.Windows.Forms.RadioButton rEraser;
        private System.Windows.Forms.RadioButton rPen;
        private System.Windows.Forms.ComboBox cColor;
        private System.Windows.Forms.ComboBox cForm;
        private System.Windows.Forms.ComboBox cSize;
        private System.Windows.Forms.RadioButton rWandPen;
        private System.Windows.Forms.TextBox wpOffset;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem статистикаToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox PathRight;
        private System.Windows.Forms.TextBox PathLeft;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem информацияToolStripMenuItem;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RadioButton rrright;
        private System.Windows.Forms.RadioButton rrleft;
        private System.Windows.Forms.Button rotleft;
        private System.Windows.Forms.Button rotright;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.CheckedListBox masks;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStripMenuItem dИзображениеToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button_to3D;
        private System.Windows.Forms.RadioButton radioButton_knife;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioButton_x_z;
        private System.Windows.Forms.RadioButton radioButton_y_z;
        private System.Windows.Forms.RadioButton radiobutton_x_y;
        private System.Windows.Forms.Button button_repaint;
    }
}

