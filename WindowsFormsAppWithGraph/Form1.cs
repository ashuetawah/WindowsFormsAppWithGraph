using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Toolkit.Services.WinForms;
using Microsoft.Toolkit.Services.Services.MicrosoftGraph;

namespace WindowsFormsAppWithGraph
{
    public partial class Form1 : Form
    {
        private Microsoft.Graph.GraphServiceClient graphClient = null;
        public Form1()
        {
            InitializeComponent();
            graphLoginComponent1.ClientId = "<Your Application ID>";
            graphLoginComponent1.Scopes = new string[] { MicrosoftGraphScope.UserRead, MicrosoftGraphScope.FilesReadAll };
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!await graphLoginComponent1.LoginAsync())
            {
                return;
            }
            //update the user's display fields
            label1.Text = graphLoginComponent1.DisplayName;
            label2.Text = graphLoginComponent1.JobTitle;
            pictureBox1.Image = graphLoginComponent1.Photo;
            // Do more things with the graph
            graphClient = graphLoginComponent1.GraphServiceClient;
            var rootItems = await graphClient.Me.Drive.Root.Children.Request().GetAsync();
            BindingSource bindingSource1 = new BindingSource();
            dataGridView1.DataSource = bindingSource1;
            bindingSource1.DataSource = rootItems;
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
        }
    }
}
