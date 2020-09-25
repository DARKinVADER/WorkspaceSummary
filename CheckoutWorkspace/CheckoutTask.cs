using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricentis.TCAPI;
using Tricentis.TCAPIObjects.Objects;

namespace CheckoutWorkspace
{
    class CheckoutTask
    {
        static void Main(string[] args)
        {
            TCAPI.CreateInstance(new Tricentis.TCAPIObjects.TCAPIConnectionInfo());
            Console.WriteLine("Enter the workspace path:");
            var path = Console.ReadLine();
            Console.WriteLine("Enter the username:");
            var user = Console.ReadLine();
            Console.WriteLine("Enter the password:");
            var password = Console.ReadLine();

            TCWorkspace workspace = TCAPI.Instance.OpenWorkspace(path, user, password);
            TCProject project = workspace.GetProject();

            if (project.IsTaskApplicable(TCTasks.Checkout))
            {
                project.Checkout();
                Console.WriteLine("Checkout succesful!");
            }
            else
            {
                Console.WriteLine("Workspace is already checked out!");
            }
            TCAPI.Instance.CloseWorkspace();
            TCAPI.CloseInstance();
        }
    }
}
