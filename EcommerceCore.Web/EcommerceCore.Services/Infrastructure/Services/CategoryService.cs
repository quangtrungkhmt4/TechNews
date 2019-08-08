using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceCore.Common.Service;
using EcommerceCore.Domain.Entities;
using EcommerceCore.Services.Infrastructure.Dto;
using EcommerceCore.Services.Infrastructure.Repositories;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;

namespace EcommerceCore.Services.Infrastructure.Services
{
    public class CategoryService: EntityService<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository) : base(categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Category GetByProduct(Guid productId)
        {
            throw new NotImplementedException();
        }

        public DataTable ReadFromExcelfile(string path, string sheetName)
        {
            DataTable dt = new DataTable();           
            using (ExcelPackage package = new ExcelPackage(new FileInfo(path)))
            {
                if (package.Workbook.Worksheets.Count < 1)
                {                    
                    return null;
                }
                // Khởi Lấy Sheet đầu tiện trong file Excel để truy vấn, 
                //truyền vào name của Sheet để lấy ra sheet cần, nếu name = null thì lấy sheet đầu tiên
                ExcelWorksheet workSheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == sheetName) ??
                                           package.Workbook.Worksheets.FirstOrDefault();
                // Đọc tất cả các header
                foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
                {
                    dt.Columns.Add(firstRowCell.Text);
                }
                // Đọc tất cả data bắt đầu từ row thứ 2
                for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
                {
                    // Lấy 1 row trong excel để truy vấn
                    var row = workSheet.Cells[rowNumber, 1, rowNumber, workSheet.Dimension.End.Column];
                    // tạo 1 row trong data table
                    var newRow = dt.NewRow();
                    foreach (var cell in row)
                    {
                        newRow[cell.Start.Column - 1] = cell.Text;

                    }
                    dt.Rows.Add(newRow);
                }
            }
            return dt;
        }

        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<CategoryDto> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 10;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "Name";
            worksheet.Cells[1, 2].Value = "Mô tả";
            worksheet.Cells[1, 3].Value = "Ngày tạo";
            // Đỗ dữ liệu từ list vào 
            for (int i = 0; i < listItems.Count(); i++)
            {
                var item = listItems[i];
                worksheet.Cells[i + 2, 1].Value = item.Name ;
                worksheet.Cells[i + 2, 2].Value = item.Description;
                worksheet.Cells[i + 2, 3].Value = item.CreatedDate;
                worksheet.Cells[i + 2, 3].Style.Numberformat.Format = "dd/MM/yyyy";
                worksheet.Cells[i + 2, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }

            //using (ExcelRange col = worksheet.Cells[2, 3, 2 + listItems.Rows.Count, 1])
            //{
            //    col.Style.Numberformat.Format = "dd/MM/yyyy HH:mm";
            //    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            //}
        }

        public Stream CreateExcelFile(Stream stream = null)
        {
            var categories = _categoryRepository.GetAll();

            var categoryDto = Mapper.Map<IEnumerable<CategoryDto>>(categories);

            //var list = categoryDto;
            using (var excelPackage = new ExcelPackage(stream ?? new MemoryStream()))
            {
                // Tạo author cho file Excel
                excelPackage.Workbook.Properties.Author = "Hanker";
                // Tạo title cho file Excel
                excelPackage.Workbook.Properties.Title = "EPP test background";              
                // Add Sheet vào file Excel
                excelPackage.Workbook.Worksheets.Add("First Sheet");
                // Lấy Sheet bạn vừa mới tạo ra để thao tác 
                var workSheet = excelPackage.Workbook.Worksheets[1];
                // Đổ data vào Excel file
                workSheet.Cells[1, 1].LoadFromCollection(categoryDto, true, TableStyles.Dark9);
                
                BindingFormatForExcel(workSheet, categoryDto.ToList());
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }
    }
}
