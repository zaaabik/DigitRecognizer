using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Core.Dto.Response;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Core.Clients {
    public class NeuralClient {
        private readonly string _neuralApiUrl;

        public NeuralClient(string neuralApiUrl) {
            _neuralApiUrl = neuralApiUrl;
        }

        public PredictDto Predict(IFormFile image) {
            byte[] data;
            using (var br = new BinaryReader(image.OpenReadStream()))
                data = br.ReadBytes((int) image.OpenReadStream().Length);
            var bytes = new ByteArrayContent(data);
            using (var client = new HttpClient()) {
                bytes.Headers.ContentType = MediaTypeHeaderValue.Parse("image/png");
                var requestContent = new MultipartFormDataContent {{bytes, "image", "image.png"}};
                var reponse = client.PostAsync(_neuralApiUrl, requestContent).GetAwaiter().GetResult();
                var reponseMessage = reponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var emailsByUserIdsResponse = JsonConvert.DeserializeObject<PredictDto>(reponseMessage);
                return emailsByUserIdsResponse;
            }
        }
    }
}