using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricentis.TCAddOns;
using Tricentis.TCAPIObjects.Objects;

namespace WorkspaceSummary
{
    class RibbonTaskWorkspaceSummary : TCAddOnMenuItem
    {
        public override string ID => "Workspace Summary";

        public override string MenuText => "Workspace Summary";

        public override void Execute(TCAddOnTaskContext context)
        {
            StringBuilder summaryInfo = new StringBuilder();
            TCProject project = TCAddOn.ActiveWorkspace.GetTCProject();
            List<string> totalTcObjectCount = new List<string>();

            totalTcObjectCount.Add(" Requirements: " + project.Search("=>SUBPARTS:Requirement").Count());
            totalTcObjectCount.Add(" TestSheet: " + project.Search("=>SUBPARTS:TestSheet").Count());
            totalTcObjectCount.Add(" XModule: " + project.Search("=>SUBPARTS:XModule").Count());
            totalTcObjectCount.Add(" TestCase: " + project.Search("=>SUBPARTS:TestCase").Count());
            totalTcObjectCount.Add(" ExecutionList: " + project.Search("=>SUBPARTS:ExecutionList").Count());

            foreach (var item in totalTcObjectCount)
            {
                summaryInfo.Append(item + Environment.NewLine);
            }

            context.ShowMessageBox($"Summary of {project.DisplayedName}", $"Count of Tosca objects at {DateTime.Now + Environment.NewLine + Environment.NewLine + summaryInfo}");
        }
    }
}
