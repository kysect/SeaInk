using System.Collections.Generic;
using System.Threading.Tasks;
using SeaInk.Core.Models;


namespace SeaInk.Core.TableGenerationService
{
    public class TableGenerator
    {
        private readonly SheetConfiguration _sheetConfiguration;
        private readonly IReadOnlyList<string> _students;

        public TableGenerator(SheetConfiguration sheetConfiguration, IReadOnlyList<string> students)
        {
            _sheetConfiguration = sheetConfiguration;
            _students = students;
        }
        
        public async Task CreateTable()
        {
            var sheet = new SheetGenerator(_sheetConfiguration);
            
            await sheet.AuthorizeSheet();

            sheet.CreateSheet();

            sheet.AddStudents(_students);
        }
    }
}