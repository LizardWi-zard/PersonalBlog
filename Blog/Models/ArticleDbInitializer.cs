using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class ArticleDbInitializer : DropCreateDatabaseAlways<ArticleContext>
    {
        protected override void Seed(ArticleContext db)
        {
            db.Articles.Add(new Article { Name = "Про прошлое", 
                                          Content = "Начало моего пути в программирование...",
                                          MainContent = "Около двух лет назад я познакомился с Unity. " +
                                          "Тогда я понял, что всё чем я хочу заниматься - это программирование",
                                         /* Date = DateTime.Now */ });
            db.Articles.Add(new Article { Name = "Про настоящее",
                                          Content = "Сейчас я стал изучать ASP.NET и пишу свой блог...",
                                          MainContent = "В данный момент мне предложили написать собственный блог. " +
                                          "Встречается много нового материала, а с ними появляются новые проблемы",
                                           /* Date = DateTime.Now */  });
            db.Articles.Add(new Article { Name = "Про будущее",
                                          Content = "В моих планах найти работу и зарекомендовать себя",
                                          MainContent = "Примерно через пол года я планирую найти работу. " +
                                          "Если не получится, то в качестве зароботка я хочу начать выполнять заказы",
                                          /* Date = DateTime.Now */
                                         });

            base.Seed(db);
        }
    }
}
