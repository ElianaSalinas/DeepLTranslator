using DeepLTranslator.Services;
using DeepLTranslator.Models;
using System.Drawing.Drawing2D;
using System;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace DeepLTranslator
{
    public partial class MainForm : Form
    {
        private readonly DeepLService _deepLService;
        private readonly TextToSpeechService _textToSpeechService;
        private string _lastTranslatedText = string.Empty;
        private string _detectedLanguage = string.Empty;

        // Controles de la interfaz
        private TextBox _inputTextBox = null!;
        private TextBox _outputTextBox = null!;
        private ComboBox _targetLanguageComboBox = null!;
        private Label _detectedLanguageLabel = null!;
        private Button _translateButton = null!;
        private Button _listenButton = null!;
        private Button _copyButton = null!;
        private Button _clearButton = null!;
        private ProgressBar _progressBar = null!;

        public MainForm()
        {
            _deepLService = new DeepLService("2afcf527-da2a-4209-9fd3-cdb7d68242f7:fx");
            _textToSpeechService = new TextToSpeechService();

            InitializeComponent();
            SetupModernUI();
            LoadTargetLanguages();
        }


        private void CreateControls()
        {
            // Panel principal
            var mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.Transparent
            };

            // Título
            var titleLabel = new Label
            {
                Text = "🌐 DeepL Translator",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                AutoSize = true,
                Location = new Point(0, 0)
            };

            // Área de texto de entrada
            var inputLabel = new Label
            {
                Text = "Enter text to translate:",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.FromArgb(73, 80, 87),
                AutoSize = true,
                Location = new Point(0, 50)
            };

            _inputTextBox = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Segoe UI", 11F),
                BorderStyle = BorderStyle.None,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(33, 37, 41),
                Location = new Point(0, 75),
                Size = new Size(840, 120),
                Padding = new Padding(15)
            };

            // Panel para idioma detectado
            var detectedPanel = new Panel
            {
                Location = new Point(0, 205),
                Size = new Size(200, 35),
                BackColor = Color.FromArgb(232, 244, 253),
                Padding = new Padding(10, 8, 10, 8)
            };

            _detectedLanguageLabel = new Label
            {
                Text = "🌐 Auto-detect",
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(13, 110, 253),
                AutoSize = true,
                Location = new Point(10, 8)
            };

            // Selector de idioma de destino
            var targetLabel = new Label
            {
                Text = "Translate to:",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.FromArgb(73, 80, 87),
                AutoSize = true,
                Location = new Point(0, 250)
            };

            _targetLanguageComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F),
                Location = new Point(0, 275),
                Size = new Size(250, 30),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(33, 37, 41)
            };

            // Botones
            _translateButton = CreateModernButton("🔄 Translate", new Point(270, 275), new Size(120, 35), Color.FromArgb(13, 110, 253));
            _clearButton = CreateModernButton("🗑️ Clear", new Point(400, 275), new Size(100, 35), Color.FromArgb(108, 117, 125));

            // Barra de progreso
            _progressBar = new ProgressBar
            {
                Location = new Point(0, 320),
                Size = new Size(840, 5),
                Style = ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 30,
                Visible = false
            };

            // Área de texto de salida
            var outputLabel = new Label
            {
                Text = "Translation:",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.FromArgb(73, 80, 87),
                AutoSize = true,
                Location = new Point(0, 335)
            };

            _outputTextBox = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Segoe UI", 11F),
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(248, 249, 250),
                ForeColor = Color.FromArgb(33, 37, 41),
                Location = new Point(0, 360),
                Size = new Size(840, 120),
                ReadOnly = true,
                Padding = new Padding(15)
            };

            // Botones de acción para la traducción
            _listenButton = CreateModernButton("🔊 Listen", new Point(0, 490), new Size(100, 35), Color.FromArgb(25, 135, 84));
            _copyButton = CreateModernButton("📋 Copy", new Point(110, 490), new Size(100, 35), Color.FromArgb(255, 193, 7));

            // Agregar controles al panel
            detectedPanel.Controls.Add(_detectedLanguageLabel);

            mainPanel.Controls.AddRange(new Control[]
            {
                titleLabel, inputLabel, _inputTextBox, detectedPanel, targetLabel,
                _targetLanguageComboBox, _translateButton, _clearButton, _progressBar,
                outputLabel, _outputTextBox, _listenButton, _copyButton
            });

            this.Controls.Add(mainPanel);
        }

        private Button CreateModernButton(string text, Point location, Size size, Color backColor)
        {
            var button = new Button
            {
                Text = text,
                Location = location,
                Size = size,
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                Cursor = Cursors.Hand
            };

            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(backColor, 0.1f);
            button.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(backColor, 0.1f);

            return button;
        }

        private void SetupModernUI()
        {
            // Aplicar bordes redondeados a los TextBox
            ApplyRoundedCorners(_inputTextBox, 8);
            ApplyRoundedCorners(_outputTextBox, 8);
            ApplyRoundedCorners(_targetLanguageComboBox, 6);

            // Aplicar bordes redondeados a los botones
            ApplyRoundedCorners(_translateButton, 6);
            ApplyRoundedCorners(_clearButton, 6);
            ApplyRoundedCorners(_listenButton, 6);
            ApplyRoundedCorners(_copyButton, 6);
        }

        private void ApplyRoundedCorners(Control control, int radius)
        {
            var path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            control.Region = new Region(path);
        }

        private void SetupLayout()
        {
            // El layout ya está configurado en CreateControls()
        }

        private void SetupEventHandlers()
        {
            _translateButton.Click += TranslateButton_Click;
            _clearButton.Click += ClearButton_Click;
            _listenButton.Click += ListenButton_Click;
            _copyButton.Click += CopyButton_Click;
            _inputTextBox.TextChanged += InputTextBox_TextChanged;

            // Permitir traducir con Enter (Ctrl+Enter)
            _inputTextBox.KeyDown += InputTextBox_KeyDown;
        }

        private void LoadTargetLanguages()
        {
            var languages = LanguageService.GetLanguagesWithFlags();

            foreach (var language in languages)
            {
                _targetLanguageComboBox.Items.Add($"{language.FlagEmoji} {language.Name}");
            }

            // Seleccionar inglés por defecto
            _targetLanguageComboBox.SelectedIndex = 0;
        }

        private async void TranslateButton_Click(object? sender, EventArgs e)
        {
            await TranslateText();
        }

        private async Task TranslateText()
        {
            if (string.IsNullOrWhiteSpace(_inputTextBox.Text))
            {
                MessageBox.Show("Please enter text to translate.", "No Text", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_targetLanguageComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a target language.", "No Language Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Mostrar progreso
                _progressBar.Visible = true;
                _translateButton.Enabled = false;
                _translateButton.Text = "Translating...";

                // Obtener el código del idioma seleccionado
                var languages = LanguageService.GetLanguagesWithFlags();
                var selectedLanguage = languages[_targetLanguageComboBox.SelectedIndex];

                // Realizar la traducción
                var (translatedText, detectedLanguage) = await _deepLService.TranslateTextAsync(
                    _inputTextBox.Text,
                    selectedLanguage.Code);

                // Mostrar resultados
                _outputTextBox.Text = translatedText;
                _lastTranslatedText = translatedText;
                _detectedLanguage = detectedLanguage;

                // Actualizar etiqueta de idioma detectado
                var detectedLanguageName = LanguageService.GetLanguageName(detectedLanguage);
                var detectedLanguageFlag = LanguageService.GetLanguageFlag(detectedLanguage);
                _detectedLanguageLabel.Text = $"{detectedLanguageFlag} {detectedLanguageName}";

                // Habilitar botones de acción
                _listenButton.Enabled = !string.IsNullOrEmpty(translatedText);
                _copyButton.Enabled = !string.IsNullOrEmpty(translatedText);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Translation failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Ocultar progreso
                _progressBar.Visible = false;
                _translateButton.Enabled = true;
                _translateButton.Text = "🔄 Translate";
            }
        }

        private void ClearButton_Click(object? sender, EventArgs e)
        {
            _inputTextBox.Clear();
            _outputTextBox.Clear();
            _detectedLanguageLabel.Text = "🌐 Auto-detect";
            _listenButton.Enabled = false;
            _copyButton.Enabled = false;
            _inputTextBox.Focus();
        }

        private async void ListenButton_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_lastTranslatedText))
                return;

            try
            {
                _listenButton.Enabled = false;
                _listenButton.Text = "🔊 Playing...";

                var languages = LanguageService.GetLanguagesWithFlags();
                var selectedLanguage = languages[_targetLanguageComboBox.SelectedIndex];

                await _textToSpeechService.SpeakAsync(_lastTranslatedText, selectedLanguage.Code);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Text-to-speech failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _listenButton.Enabled = true;
                _listenButton.Text = "🔊 Listen";
            }
        }

        private void CopyButton_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_lastTranslatedText))
                return;

            try
            {
                Clipboard.SetText(_lastTranslatedText);

                // Feedback visual temporal
                var originalText = _copyButton.Text;
                _copyButton.Text = "✅ Copied!";
                _copyButton.BackColor = Color.FromArgb(25, 135, 84);

                var timer = new System.Windows.Forms.Timer { Interval = 2000 };
                timer.Tick += (s, e) =>
                {
                    _copyButton.Text = originalText;
                    _copyButton.BackColor = Color.FromArgb(255, 193, 7);
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to copy text: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InputTextBox_TextChanged(object? sender, EventArgs e)
        {
            // Resetear idioma detectado cuando cambia el texto
            if (string.IsNullOrWhiteSpace(_inputTextBox.Text))
            {
                _detectedLanguageLabel.Text = "🌐 Auto-detect";
            }
        }

        private async void InputTextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            // Traducir con Ctrl+Enter
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                await TranslateText();
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _deepLService?.Dispose();
            _textToSpeechService?.Dispose();
            base.OnFormClosed(e);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
