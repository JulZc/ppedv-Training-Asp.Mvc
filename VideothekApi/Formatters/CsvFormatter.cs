using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using VideothekData.Models;

namespace VideothekApi.Formatters
{
    public class CsvFormatter : BufferedMediaTypeFormatter
    {
        public CsvFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            if (type == typeof(Movie))
                return true;

            Type listMovie = typeof(IEnumerable<Movie>);
            return listMovie.IsAssignableFrom(type);
        }


        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            using (StreamWriter writer = new StreamWriter(writeStream))
            {
                writer.WriteLine("Id,Name,NumberInStock,Released");
                List<Movie> movies = value as List<Movie>;

                if (movies != null)
                {
                    foreach (var movie in movies)
                    {
                        WriteItemToStream(movie, writer);
                    }
                }
                else
                {
                    Movie movie = (Movie)value;
                    WriteItemToStream(movie, writer);
                }
            }
        }


        private void WriteItemToStream(Movie movie, StreamWriter writer)
        {
            writer.WriteLine($"{movie.Id},{movie.Name},{movie.NumberInStock},{movie.Released}");
        }
    }
}