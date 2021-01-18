using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace AI900_LAB01
{
    class Program
    {
        const string key = "2375ed7f0bfe485ea97bfa55cc53cb91";
        const string endpoint = "https://sg-mlai900.cognitiveservices.azure.com/";
        static void Main(string[] args)
        {
            IFaceClient client = AuthFace(endpoint, key);

            DetectarFaces(client, "recognition_03").Wait();

            Console.ReadKey();
        }

        private static async Task DetectarFaces(IFaceClient client, string recognitionModel)
        {
            Console.WriteLine("Detectando faces");

            List<string> imagesList = new List<string>
            {
                 "https://www.sunoresearch.com.br/wp-content/uploads/2018/08/jeff-bezos-3.jpg"
            };

            foreach (var image in imagesList)
            {
                IList<DetectedFace> detectedFaces;

                detectedFaces = await client.Face.DetectWithUrlAsync(image, 
                    returnFaceAttributes: new List<FaceAttributeType?> { FaceAttributeType.Accessories, FaceAttributeType.Age,
                FaceAttributeType.Blur, FaceAttributeType.Emotion, FaceAttributeType.Exposure, FaceAttributeType.FacialHair,
                FaceAttributeType.Gender, FaceAttributeType.Glasses, FaceAttributeType.Hair, FaceAttributeType.HeadPose,
                FaceAttributeType.Makeup, FaceAttributeType.Noise, FaceAttributeType.Occlusion, FaceAttributeType.Smile },
                        detectionModel: DetectionModel.Detection01,
                        recognitionModel: recognitionModel);

                Console.WriteLine($"{detectedFaces.Count} face(s) encontradas na images.");
                Console.WriteLine($"Idade: {detectedFaces.FirstOrDefault().FaceAttributes.Age}");
                Console.WriteLine($"Sexo: {detectedFaces.FirstOrDefault().FaceAttributes.Gender}");

                //Cabelo
                //Console.WriteLine($"Cor do Cabelo: {detectedFaces.FirstOrDefault().FaceAttributes.Hair.HairColor}");
                //Console.WriteLine($"Careca: {detectedFaces.FirstOrDefault().FaceAttributes.Hair.Bald}");
                //Maquiagem
                Console.WriteLine($"Batom: {detectedFaces.FirstOrDefault().FaceAttributes.Makeup.LipMakeup}");
                Console.WriteLine($"Lápis nos Olhos: {detectedFaces.FirstOrDefault().FaceAttributes.Makeup.EyeMakeup}");
                //Emoções
                Console.WriteLine($"Sorriso: {detectedFaces.FirstOrDefault().FaceAttributes.Smile}");
                Console.WriteLine($"Emoção Feliz: {detectedFaces.FirstOrDefault().FaceAttributes.Emotion.Happiness}");
                Console.WriteLine($"Emoção Neutro: {detectedFaces.FirstOrDefault().FaceAttributes.Emotion.Neutral}");
                Console.WriteLine($"Emoção Surpreso: {detectedFaces.FirstOrDefault().FaceAttributes.Emotion.Surprise}");
                Console.WriteLine($"Emoção Triste: {detectedFaces.FirstOrDefault().FaceAttributes.Emotion.Sadness}");
                Console.WriteLine($"Emoção Com raiva: {detectedFaces.FirstOrDefault().FaceAttributes.Emotion.Anger}");

            }

        }

        private static IFaceClient AuthFace(string endpoint, string key)
        {
            return new FaceClient(new ApiKeyServiceClientCredentials(key)) { Endpoint = endpoint };
        }
    }
}
