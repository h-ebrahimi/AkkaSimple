using Akka.Configuration;

namespace ChatServer
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
                            port = 8081 #bound to a specific port
                            hostname = localhost
                        }
                    }
                }
            ");

        public static string Nickname => "Server";
    }
}
