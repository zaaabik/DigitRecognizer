using DigitRecognizer.Dto.Response;
using Microsoft.AspNetCore.Http;

namespace DigitRecognizer.Services.Interface {
    public interface INeuralService {
        PredictDto Predict(IFormFile image);
    }
}