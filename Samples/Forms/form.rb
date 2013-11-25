form = System.Windows.Forms.Form.new
button = System.Windows.Forms.Button.new
button.Text = "Hello!"
form.Text = "Hello Form"
form.Controls.Add(button)
button.Dock = System.Windows.Forms.DockStyle::Fill
System.Windows.Forms.Application.Run(form)
