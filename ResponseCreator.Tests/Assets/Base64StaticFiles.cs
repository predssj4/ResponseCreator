﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ResponseCreator.Tests.Assets
{
    public class Base64StaticFiles
    {
        // name: "image4 - Copy.jpg"
        public static string jpgFile1 = "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";

        // name: NHibernateMappingGenerator.exe.config
        public static string configFile1 =
            "PD94bWwgdmVyc2lvbj0iMS4wIj8+DQo8Y29uZmlndXJhdGlvbj4NCjxzdGFydHVwPjxzdXBwb3J0ZWRSdW50aW1lIHZlcnNpb249InY0LjAiIHNrdT0iLk5FVEZyYW1ld29yayxWZXJzaW9uPXY0LjAiLz48L3N0YXJ0dXA+PC9jb25maWd1cmF0aW9uPg0K";

    }
}
