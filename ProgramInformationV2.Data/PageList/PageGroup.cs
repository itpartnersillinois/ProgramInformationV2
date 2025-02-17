namespace ProgramInformationV2.Data.PageList {

    public static class PageGroup {

        private static readonly Dictionary<string, List<PageLink>> _sidebars = new() {
            // first one is the page title, rest are the other links -- page title can be overwritten
            { "editinformation", new() { new ("Edit Information", "/editinformation"),
                                     new ("Programs & Credentials", "/programcred"),
                                     new ("Courses & Sections", "/coursesection"),
                                     new ("Requirement Sets", "/requirementset") } },
            { "programcred", new() { new ("Programs And Credentials", "/programcred"),
                                     new ("Programs", "/programcred/program"),
                                     new ("Credentials", "/programcred/credential") } },
            { "program", new() { new ("Program", "/programcred/program"),
                                 new ("General Information", "/programcred/program/general"),
                                 new ("Link Information", "/programcred/program/link"),
                                 new ("Overview", "/programcred/program/overview"),
                                 new ("Filters", "/programcred/program/filters"),
                                 new ("Credential List", "/programcred/program/credentiallist"),
                                 new ("Technical Details", "/programcred/program/technical") } },
            { "credential", new() { new ("Credential", "/programcred/credential"),
                                 new ("General Information", "/programcred/credential/general"),
                                 new ("Link Information", "/programcred/credential/link"),
                                 new ("Overview", "/programcred/credential/overview"),
                                 new ("Filters", "/programcred/credential/filters"),
                                 new ("Course List", "/programcred/credential/courselist"),
                                 new ("Transcript Information", "/programcred/credential/transcript"),
                                 new ("Technical Details", "/programcred/credential/technical") } },
            { "configuration", new() { new ("Configuration", "/configuration/sources"),
                                 new ("Sources", "/configuration/sources"),
                                 new ("Fields Used", "/configuration/fieldsused"),
                                 new ("Manage Filters", "/configuration/filters"),
                                 new ("Security", "/configuration/security"),
                                 new ("Request Deletion", "/configuration/requestdeletion"),
                                 new ("Testing Access", "/configuration/testing"),
                                 new ("Save Information to JSON", "/configuration/savejson"),
                                 new ("Load JSON Information to the Server", "/configuration/loadjson") } }
        };

        private static readonly Dictionary<string, PageLink> _urls = new() {
            { "home", new ("Home", "/") },
            { "editinformation", new ("Edit Information", "/editinformation") },
            { "programcred", new ("Programs and Credentials", "/programcred") },
            { "program", new ("Programs", "/programcred/program") },
            { "credential", new ("Credentials", "/programcred/credential") },
            { "configuration", new ("Configuration", "/configuration/sources") }
        };

        public static PageLink? Get(string s) => _urls.ContainsKey(s) ? _urls[s] : null;

        public static List<PageLink>? GetSidebar(string s) => _sidebars.ContainsKey(s) ? _sidebars[s].Skip(1).ToList() : null;

        public static PageLink? GetSidebarTitle(string s) => _sidebars.ContainsKey(s) ? _sidebars[s].First() : null;
    }
}