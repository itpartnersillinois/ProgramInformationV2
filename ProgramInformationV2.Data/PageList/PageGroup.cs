namespace ProgramInformationV2.Data.PageList {

    public static class PageGroup {

        private static readonly Dictionary<SidebarEnum, List<PageLink>> _sidebars = new() {
            // first one is the page title, rest are the other links -- page title can be overwritten
            { SidebarEnum.EditInformation, new() { new ("Edit Information", "/editinformation"),
                                     new ("Programs & Credentials", "/programcred"),
                                     new ("Courses & Sections", "/coursesection"),
                                     new ("Requirement Sets", "/requirementset") } },
            { SidebarEnum.ProgramCredential, new() { new ("Programs And Credentials", "/programcred"),
                                     new ("Programs", "/programs"),
                                     new ("Credentials", "/credentials") } },
            { SidebarEnum.Program, new() { new ("Program", "/program"),
                                 new ("General Information", "/program/general"),
                                 new ("Link Information", "/program/link"),
                                 new ("Overview", "/program/overview"),
                                 new ("Filters", "/program/filters"),
                                 new ("Credential List", "/program/credentiallist"),
                                 new ("Technical Details", "/program/technical") } },
            { SidebarEnum.Credential, new() { new ("Credential", "/credential"),
                                 new ("General Information", "/credential/general"),
                                 new ("Link Information", "/credential/link"),
                                 new ("Overview", "/credential/overview"),
                                 new ("Filters", "/credential/filters"),
                                 new ("Course List", "/credential/courselist"),
                                 new ("Transcript Information", "/credential/transcript"),
                                 new ("Technical Details", "/credential/technical") } },
            { SidebarEnum.Configuration, new() { new ("Configuration", "/configuration/sources"),
                                 new ("Sources", "/configuration/sources"),
                                 new ("Fields Used", "/configuration/fieldsused/programs"),
                                 new ("Manage Filters", "/configuration/filters"),
                                 new ("Security", "/configuration/security"),
                                 new ("Request Deletion", "/configuration/requestdeletion"),
                                 new ("Testing Access", "/configuration/testing"),
                                 new ("Save Information to JSON", "/configuration/savejson"),
                                 new ("Load JSON Information to the Server", "/configuration/loadjson") } },
            { SidebarEnum.FieldsUsed, new() { new ("Fields Used", "/configuration/fieldsused/programs"),
                                 new ("Programs", "/configuration/fieldsused/programs"),
                                 new ("Credentials", "/configuration/fieldsused/credentials"),
                                 new ("Courses", "/configuration/fieldsused/courses"),
                                 new ("Sections", "/configuration/fieldsused/sections"),
                                 new ("Requirement Sets", "/configuration/fieldsused/requirementsets") } }
        };

        private static readonly Dictionary<SidebarEnum, List<PageLink>> _breadcrumbs = new() {
            { SidebarEnum.Configuration, new() { new ("Home", "/"),
                                 new ("Configuration", "/configuration/sources") } },
            { SidebarEnum.FieldsUsed, new() { new ("Home", "/"),
                                 new ("Configuration", "/configuration/sources"),
                                 new ("Fields Used", "/configuration/fieldsused/programs") } }
        };

        public static List<PageLink>? GetBreadcrumbs(SidebarEnum s) => _breadcrumbs.ContainsKey(s) ? _breadcrumbs[s] : null;

        public static List<PageLink>? GetSidebar(SidebarEnum s) => _sidebars.ContainsKey(s) ? _sidebars[s].Skip(1).ToList() : null;

        public static PageLink? GetSidebarTitle(SidebarEnum s) => _sidebars.ContainsKey(s) ? _sidebars[s].First() : null;
    }
}