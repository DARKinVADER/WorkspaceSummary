using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricentis.TCAddOns;
using Tricentis.TCAPIObjects.Objects;

namespace TestCaseFromModule_DropTask
{
    class DropTaskCreateTestCase : TCAddOnDropTask
    {
        public override string Name => "Create TestCase from Module";

        public override Type ApplicableType => typeof(TCFolder);

        public override Type DropObjectType => typeof(TCFolder);
        public override bool IsTaskPossible(TCObject targetObject, TCObject sourceObject)
        {
            TCFolder moduleFolder = sourceObject as TCFolder;
            TCFolder testCaseFolder = targetObject as TCFolder;

            if(moduleFolder==null || testCaseFolder == null)
            {
                return false;
            }

            if(moduleFolder.PossibleContent.Equals("Folder;Module") && testCaseFolder.PossibleContent.Equals("Folder;TestCase"))
            {
                return true;
            }

            return false;
        }

        public override TCObject Execute(TCObject obj, List<TCObject> dropObjects, bool copy, TCAddOnTaskContext context)
        {
            TCFolder folder = (TCFolder)obj;
            TestCase testCase = folder.CreateTestCase();

            testCase.Name = "Drop Task AddOn";
            testCase.EnsureUniqueName();

            foreach (var objDrop in dropObjects)
            {
                TCFolder moduleFolder = objDrop as TCFolder;
                if (moduleFolder == null)
                {
                    continue;
                }
                List<TCObject> modules = moduleFolder.Search("=>SUBPARTS:XModule");
                testCase.CreateXTestStepFromXModule(modules);
            }

            return null;
        }
    }
}
