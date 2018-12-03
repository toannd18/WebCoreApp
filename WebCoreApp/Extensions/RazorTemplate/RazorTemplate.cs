using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RazorLight;


namespace WebCoreApp.Extensions.RazorTemplate
{
    public class RazorTemplate : IRazorTemplate
    {

        public async Task<string> Templates(string PathName, object model,string key)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates");
            RazorLightEngine engine = new RazorLightEngineBuilder()
                .UseFilesystemProject(path)
                .UseMemoryCachingProvider()
                .Build();
            var cacheResult = engine.TemplateCache.RetrieveTemplate(key);
            string result;
            if (cacheResult.Success)
            {
                 result = await engine.RenderTemplateAsync(cacheResult.Template.TemplatePageFactory(), model);
            }
            else
            {
               result = await engine.CompileRenderAsync("templateKey", PathName, model);
            }
            return result;
        }
    }
}
