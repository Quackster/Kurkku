using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Kurkku.Game
{
    public class PermissionsManager : ILoadable
    {
        #region Fields

        public static readonly PermissionsManager Instance = new PermissionsManager();

        #endregion

        #region Properties

        public Dictionary<int, UserGroup> Ranks;

        #endregion

        #region Constructors

        public PermissionsManager() { }

        #endregion

        #region Public methods

        public void Load()
        {
            Ranks = new Dictionary<int, UserGroup>();

            var input = new StringReader(File.ReadAllText("permissions.yml"));
            var deserializer = new DeserializerBuilder().Build();

            var config = deserializer.Deserialize<Dictionary<string, Dictionary<string, dynamic>>>(input);
            var groups = config["groups"];

            foreach (var kvp in groups)
            {
                var groupName = kvp.Key;
                var groupData = kvp.Value;

                var rank = int.Parse(groupData["rank"]);
                var userGroup = new UserGroup(groupName, rank);

                try
                {
                    var permissions = groupData["permissions"];
                    if (permissions != null)
                    {
                        foreach (var permission in permissions)
                            userGroup.DefaultPermissions.Add(permission);
                    }
                }
                catch { }

                try
                {
                    var inherits = groupData["inherits"];
                    if (inherits != null)
                    {
                        foreach (var inheritsGroup in inherits)
                            userGroup.InheritsGroups.Add(inheritsGroup);
                    }
                }
                catch { }

                try
                {
                    var excludes = groupData["excludes"];
                    if (excludes != null)
                    {
                        foreach (var excludesGroup in excludes)
                            userGroup.ExcludesPermissions.Add(excludesGroup);
                    }
                }
                catch { }

                Ranks[rank] = userGroup;
            }

            foreach (var rank in Ranks.Values.ToArray())
            {
                rank.BuildPermissions(Ranks.Values.ToArray());
            }

        }

        #endregion
    }
}
