using Akka.Configuration;
using Akka.Configuration.Hocon;

namespace ClusterConfig
{
    public static class SeedNode
    {
        public static Config ConfigPath =>
            ConfigurationFactory.ParseString(@"
                akka {
                    actor.provider = cluster
                    remote {
                        dot-netty.tcp {
                            port = 8081
                            hostname = localhost
                        }
                    }
                    cluster {
                        seed-nodes = [""akka.tcp://ClusterSystem@localhost:8081""]
                    }
                }
            ");

        public static Config ConfigApp = new Config
        {
            

        };

       

        public static string Nickname => "Server";

        public static void Do()
        {

            //ConfigApp.Root.AppendValue();
        }
    }
}