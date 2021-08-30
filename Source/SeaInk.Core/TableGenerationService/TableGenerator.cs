using System.Collections.Generic;
using System.Threading.Tasks;
using SeaInk.Core.Models;


namespace SeaInk.Core.TableGenerationService
{
    public class TableGenerator
    {
        public SheetConfiguration SheetConfiguration { get; }
        public IReadOnlyList<string> Students { get; }

        public TableGenerator(SheetConfiguration sheetConfiguration, List<string> students)
        {
            SheetConfiguration = sheetConfiguration;
            Students = students;
        }
        
        public async Task CreateTable()
        {
            var sheet = new SheetGenerator(SheetConfiguration);
            
            await sheet.AuthorizeSheet();

            sheet.CreateSheet();

            sheet.AddStudents(Students);
        }
    }
}