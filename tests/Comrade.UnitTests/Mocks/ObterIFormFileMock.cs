#region

using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Comrade.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Moq;

#endregion

namespace Comrade.UnitTests.Mocks
{
    public class ObterIFormFileMock
    {
        public async Task<IFormFile> Execute()
        {
            var fileMock = new Mock<IFormFile>();
            var jsonPath = "Comrade.Infrastructure.SeedData.Sheets";
            var filePath = $"{jsonPath}.basicSheet.xlsx";
            var fileName = "basicSheet.xlsx";
            var assembly = Assembly.GetAssembly(typeof(JsonUtilities));
            if (assembly is not null)
            {
                var arquivo = assembly.GetManifestResourceStream(filePath);
                var ms = new MemoryStream();
                if (arquivo != null)
                {
                    await arquivo.CopyToAsync(ms);
                    ms.Position = 0;
                    fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
                    fileMock.Setup(_ => _.FileName).Returns(fileName);
                    fileMock.Setup(_ => _.Length).Returns(ms.Length);
                }
            }

            return fileMock.Object;
        }
    }
}