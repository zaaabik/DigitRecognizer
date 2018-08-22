using System;
using System.Collections.Generic;
using System.Linq;
using Core.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitRecognizer.Controllers {
    public class ImageController : Controller {
        private readonly NeuralClient _neuralClient;

        public ImageController(NeuralClient neuralService) {
            _neuralClient = neuralService;
        }

        [HttpPut("image")]
        public IActionResult SaveImage(IEnumerable<IFormFile> files) {
            Console.WriteLine("test");
            var image = Request.Form.Files.FirstOrDefault();
            if (image == null) {
                return BadRequest();
            }

            var predict = _neuralClient.Predict(image);

            return Json(new {
                Number = predict.Number,
                Probability = predict.Probability
            });
        }
    }
}