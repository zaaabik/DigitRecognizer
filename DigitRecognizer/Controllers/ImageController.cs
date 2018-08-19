using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DigitRecognizer.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitRecognizer.Controllers {
    public class ImageController : Controller {
        private readonly INeuralService _neuralService;
        public ImageController(INeuralService neuralService) {
            _neuralService = neuralService;
        }

        [HttpPut("image")]
        public IActionResult SaveImage(IEnumerable<IFormFile> files) {
            Console.WriteLine("test");
            var image = Request.Form.Files.FirstOrDefault();
            if (image == null) {
                return BadRequest();
            }

            using (var stream = new FileStream("test.png", FileMode.Create)) {
                image.CopyToAsync(stream);
            }

            var predict = _neuralService.Predict(image);

            return Json(new {
                Number = predict.Number,
                Probability = predict.Probability 
            });
        }
    }
}