using Kurkku.Storage.Database.Data;
using NHibernate.Criterion;
using NHibernate.Criterion.Lambda;
using NHibernate.Linq;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Storage.Database.Access
{
    public class TagDao
    {
        /// <summary>
        /// Delete tags for room
        /// </summary>
        public static void DeleteRoomTags(int roomId)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                session.Query<TagData>().Where(x => x.RoomId == roomId).Delete();
            }
        }

        /// <summary>
        /// Get popular tags assigned to a room.
        /// </summary>
        public static List<PopularTag> GetPopularTags(int tagLimit = 50)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                TagData tagAlias = null;
                PopularTag popularTagAlias = null;

                return session.QueryOver<TagData>(() => tagAlias)
                    .Where(x => x.RoomId > 0)
                    .SelectList(list => list
                        .Select(() => tagAlias.Text).WithAlias(() => popularTagAlias.Tag)
                        .SelectCount(() => tagAlias.RoomId).WithAlias(() => popularTagAlias.Quantity)
                        .SelectGroup(() => tagAlias.Text)

                    )
                    .OrderByAlias(() => popularTagAlias.Quantity).Desc
                    .TransformUsing(Transformers.AliasToBean<PopularTag>())
                    .Take(tagLimit)
                    .List<PopularTag>() as List<PopularTag>;
            }
        }

        /// <summary>
        /// Save the room tags
        /// </summary>
        /// <returns></returns>
        public static void SaveTag(TagData tagData)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(tagData);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        transaction.Rollback();
                    }
                }
            }
        }
    }
}
