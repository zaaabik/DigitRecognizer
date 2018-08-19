using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using DigitRecognizer.Dto.Image;
using DigitRecognizer.Dto.Response;
using DigitRecognizer.Services.Interface;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DigitRecognizer.Services.Implementation {
    public class NeuralService : INeuralService {
        public PredictDto Predict(IFormFile image) {
            byte[] data;
            using (var br = new BinaryReader(image.OpenReadStream()))
                data = br.ReadBytes((int) image.OpenReadStream().Length);
            var bytes = new ByteArrayContent(data);
            using (var client = new HttpClient()) {
                bytes.Headers.ContentType = MediaTypeHeaderValue.Parse("image/png");
                var requestContent = new MultipartFormDataContent();
                requestContent.Add(bytes, "image", "image.png");
                var url = "http://localhost:1234/neural/predict";
                var reponse = client.PostAsync(url, requestContent).GetAwaiter().GetResult();
                var reponseMessage = reponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var emailsByUserIdsResponse = JsonConvert.DeserializeObject<PredictDto>(reponseMessage);                
                return emailsByUserIdsResponse;
            }
        }
    }
}