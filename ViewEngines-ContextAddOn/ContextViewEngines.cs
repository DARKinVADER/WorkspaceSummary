using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricentis.TCAddOns;
using Tricentis.TCAPIObjects.Objects;

namespace ViewEngines_ContextAddOn
{
    class ContextViewEngines : TCAddOnTask
    {
        public override string Name => "View Engines";

        public override Type ApplicableType => typeof(TCFolder);

        public override bool IsTaskPossible(TCObject obj)
        {
            TCFolder moduleFolder = obj as TCFolder;

            if(obj == null)
            {
                return false;
            }

            if (moduleFolder.PossibleContent.Equals("Folder;Module") && ((TCFolder)obj).Search("=>SUBPARTS:XModule").Count>0)
            {
                return true;
            }

            return false;
        }

        public override TCObject Execute(TCObject objectToExecuteOn, TCAddOnTaskContext taskContext)
        {
            IEnumerable<XModule> modules = ((TCFolder)objectToExecuteOn).Search("=>SUBPARTS:XModule").OfType<XModule>();
            string engines = string.Empty;

            foreach (var item in modules)
            {
                var engineParam = item.XParams.Where(t=>t.Name == "Engine").First();
                if (!engines.Contains(engineParam.Value)){
                    engines+=(engineParam.Value + Environment.NewLine);
                }
            }

            taskContext.ShowMessageBox("Used Engines", engines);

            return objectToExecuteOn;
        }
    }
}
