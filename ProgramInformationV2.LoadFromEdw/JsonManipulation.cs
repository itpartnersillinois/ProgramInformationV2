using Newtonsoft.Json;

namespace ProgramInformationV2.LoadFromEdw {

    internal static class JsonManipulation {

        internal static void TranslateCourses(string path, string file) {
            path = NormalizePath(path);
            using var reader = new StreamReader(path + file);
            var items = JsonConvert.DeserializeObject<List<dynamic>>(reader.ReadToEnd()) ?? [];
            Console.WriteLine("File: " + file);
            Console.WriteLine("Number of items: " + items.Count);
            int i = 1;
            foreach (var item in items) {
                string s = item._source.url ?? "";
                if (string.IsNullOrWhiteSpace(s)) {
                    s = "/course/" + item._source.rubric + "/" + item._source.courseNumber;
                }

                s = s.Replace("https://education.illinois.edu/", "/");
                item._source.url = s;
                item._source.fragment = item._source.url;
                if (string.IsNullOrWhiteSpace(s)) {
                    Console.WriteLine(i++ + ". Course Title: " + item._source.title);
                } else {
                    Console.WriteLine(i++ + ". Course URL: " + item._source.url);
                }
            }
            using var writer = new StreamWriter(path + "new_" + file);
            writer.Write(JsonConvert.SerializeObject(items, Formatting.Indented));
        }

        internal static void TranslatePrograms(string path, string file) {
            path = NormalizePath(path);
            using var reader = new StreamReader(path + file);
            var items = JsonConvert.DeserializeObject<List<dynamic>>(reader.ReadToEnd()) ?? [];
            Console.WriteLine("Number of items: " + items.Count);
            int i = 1;
            foreach (var item in items) {
                item._source.fragment = item._source.url;
                Console.WriteLine(i++ + ". Program URL: " + item._source.url);
                foreach (var credential in item._source.credentials) {
                    string s = credential?.url ?? "";
                    if (!s.StartsWith("http")) {
                        credential.fragment = credential.url;
                        Console.WriteLine("Credential URL: " + credential.url);
                    }
                }
            }
            using var writer = new StreamWriter(path + "new_" + file);
            writer.Write(JsonConvert.SerializeObject(items, Formatting.Indented));
        }

        private static string NormalizePath(string s) => s.TrimEnd('\\') + "\\";
    }
}