using Akka.Configuration;

namespace ChatClient
{
    internal static class ChatConfig
    {
        public static Config ConfigPath =>
            ConfigurationFactory.ParseString(@"
                akka {  
                    actor {
                        provider = remote
                    }
                    remote {
                        dot-netty.tcp {
                            port = 8081
                            hostname = 0.0.0.0
                            public-hostname = localhost
                        }
                    }
                }
            ");

        public static string ServerPath => "akka.tcp://ServerSystem@localhost:8081/user/ChatServer";
        public static string Nickname => "Hossein Ebrahimi";
    }
}
