#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#endregion

namespace RAB_Session01Challenge
{
    [Transaction(TransactionMode.Manual)]
    public class Command01Challenge : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            // Variables (string, int, double, XYZ)
            string myString = "This is the FizzBuzz Challenge";


            // Filtered Element Collectors
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            //collector.OfCategory(BuiltInCategory.OST_TextNotes);
            //collector.WhereElementIsElementType();
            collector.OfClass(typeof(TextNoteType));

            XYZ myPoint = new XYZ(10, 10, 0);
            

            // Modify Document within a Transaction

            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Create FizzBuzz Text Note");

                XYZ offset = new XYZ(0, 10, 0);
                XYZ newPoint = myPoint;

                
                for (int i = 1; i <= 100; i++)
                {
                    newPoint = newPoint.Add(offset);

                    // Conditional Logic
                    string result = "";
                    int result2 = i;
                    if (i % 3 == 0 && i % 5 == 0)
                    {
                        result = " FIZZBUZZ";
                    }
                    else if (i % 3 == 0)
                    {
                        result = " FIZZ";
                    }
                    else if (i % 5 == 0)
                    {
                        result = " BUZZ";
                    }
                    else
                    {
                        result2 = i;
                    }

                    // Text Notes
                    TextNote myTextnote = TextNote.Create(doc, doc.ActiveView.Id,
                        newPoint, myString + i.ToString() + result, collector.FirstElementId());
                }



                

              

                tx.Commit();

            }

            return Result.Succeeded;
        }
    }
}
