using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tricentis.TCAPI;
using Tricentis.TCAPIObjects.Objects;

namespace CreateExecutionList
{
    public partial class CreateExecutionList : Form
    {
        public CreateExecutionList()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            TCAPI.CreateInstance(new Tricentis.TCAPIObjects.TCAPIConnectionInfo());

            TCWorkspace workspace = TCAPI.Instance.OpenWorkspace(txtPath.Text, txtUser.Text, txtPassword.Text);
            TCProject project = workspace.GetProject();
            
            TestCase testCase = (TestCase)project.Search($"=>SUBPARTS:TestCase[Name==\"{txtTestCase.Text}\"]").FirstOrDefault();
            if (testCase != null)
            {
                TCFolder executionListFolder = (TCFolder)project.Search("=>SUBPARTS:TCFolder[PossibleContent==\"Folder;ExecutionList\"]").First();
                if (executionListFolder.IsTaskApplicable(TCTasks.Checkout))
                {
                    executionListFolder.Checkout();                    
                }
                ExecutionList executionList = executionListFolder.CreateExecutionList();
                executionList.Name = txtTestCase.Text;
                executionList.CreateExecutionEntry(testCase);
                if (executionListFolder.CheckOutState.Equals(CheckOutState.CheckedOut))
                {
                    workspace.CheckInAll("Execution list created from code!");
                }
                workspace.Save();
                MessageBox.Show("Execution list created successfully");
            }
            else
            {
                MessageBox.Show("TestCase not found!");
            }
            TCAPI.Instance.CloseWorkspace();
            TCAPI.CloseInstance();
        }
    }
}
