using System;
using System.IO;
using System.Windows.Forms;
using PartialFileRenamer;

namespace PartialFileRenamer_GUI
{
    public partial class PartialFileRenamerGUI : Form
    {
        public PartialFileRenamerGUI()
        {
            InitializeComponent();
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                directoryTextBox.Text = folderBrowserDialog1.SelectedPath;
            }

        }

        private void Rename_Click(object sender, EventArgs e)
        {
            if(findTextBox.Text == "" || replaceTextBox.Text == "" || directoryTextBox.Text == "")
            {
                MessageBox.Show("Please make sure all text boxes are filled.");
            }
            else
            {
                if (Directory.Exists(directoryTextBox.Text))
                {
                    FileRenamer fileRenamer = new FileRenamer();

                    bool overwrite = overwriteCheckBox.Enabled;
                    bool subfolders = subfoldersCheckBox.Enabled;
                    bool contents = contentsCheckBox.Enabled;

                    fileRenamer.rename(directoryTextBox.Text, findTextBox.Text, replaceTextBox.Text, overwrite, contents, subfolders, false, false);
                }
                else
                {
                    MessageBox.Show("Please ensure the directory path is to an existing directory.");
                }

                MessageBox.Show("Rename Complete");
            }
        }
    }
}
