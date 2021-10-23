using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrayCorp.Models;
using TrayCorp.Repositorio;

namespace TrayCorp.Controllers
{
    public class ProdutosController : Controller
    {

        ProdutoDBContext db;

        public ProdutosController()
        {

            db = new ProdutoDBContext();

            if (db.PRODUTO.Count() == 0)
            {

                var produto1 = new PRODUTO();
                produto1.PROD_NOME = "Lapiseira 0.7";
                produto1.PROD_ESTOQUE = 10;
                produto1.PROD_VALOR = 10;

                db.PRODUTO.Add(produto1);

                var produto2 = new PRODUTO();
                produto2.PROD_NOME = "Lapiseira 0.5";
                produto2.PROD_ESTOQUE = 20;
                produto2.PROD_VALOR = 10;

                db.PRODUTO.Add(produto2);

                var produto3 = new PRODUTO();
                produto3.PROD_NOME = "Caneta Azul BIC";
                produto3.PROD_ESTOQUE = 20;
                produto3.PROD_VALOR = 10;

                db.PRODUTO.Add(produto3);

                var produto4 = new PRODUTO();
                produto4.PROD_NOME = "Caneta Vermelha BIC";
                produto4.PROD_ESTOQUE = 30;
                produto4.PROD_VALOR = 10;

                db.PRODUTO.Add(produto4);

                var produto5 = new PRODUTO();
                produto5.PROD_NOME = "Caneta Preta BIC";
                produto5.PROD_ESTOQUE = 10;
                produto5.PROD_VALOR = 10;

                db.PRODUTO.Add(produto5);


                db.SaveChanges();

            }

        }

        // GET: Produtos
        /*public ActionResult Index()
        {

            var produtos = db.PRODUTO.ToList();


            List<ProdutoViewModel> produtosViews = new List<ProdutoViewModel>();

            foreach ( PRODUTO item in produtos )
            {

                ProdutoViewModel temp = new ProdutoViewModel();

                temp.PROD_ID = item.PROD_ID;
                temp.PROD_NOME = item.PROD_NOME;
                temp.PROD_ESTOQUE = item.PROD_ESTOQUE;
                temp.PROD_VALOR = item.PROD_VALOR;

                produtosViews.Add(temp);


            }


            return View(produtosViews);

        }*/

        public ActionResult Create()
        {
            //ViewBag.Produto = db.PRODUTO;
            var model = new ProdutoViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProdutoViewModel model)
        {

            if ((ModelState.IsValid) && (model.PROD_VALOR >= 0))
            {

                var produto = new PRODUTO();
                produto.PROD_NOME = model.PROD_NOME;
                produto.PROD_ESTOQUE = model.PROD_ESTOQUE;
                produto.PROD_VALOR = model.PROD_VALOR;

                db.PRODUTO.Add(produto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (model.PROD_VALOR < 0)
                return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable, "O valor não pode ser negativo!");

            // Se ocorrer um erro retorna para pagina
            return View(model);
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PRODUTO produto = db.PRODUTO.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }

            ProdutoViewModel produtoView = new ProdutoViewModel();

            produtoView.PROD_ID = produto.PROD_ID;
            produtoView.PROD_NOME = produto.PROD_NOME;
            produtoView.PROD_ESTOQUE = produto.PROD_ESTOQUE;
            produtoView.PROD_VALOR = produto.PROD_VALOR;

            return View(produtoView);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PROD_ID,PROD_NOME,PROD_ESTOQUE,PROD_VALOR")] ProdutoViewModel model)
        {

            if (ModelState.IsValid)
            {

                var produto = db.PRODUTO.Find(model.PROD_ID);

                if (produto == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (model.PROD_VALOR < 0)
                    return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable, "O valor não pode ser negativo!");

                produto.PROD_NOME = model.PROD_NOME;
                produto.PROD_ESTOQUE = model.PROD_ESTOQUE;
                produto.PROD_VALOR = model.PROD_VALOR;

                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(model);
        }

        // GET: Produtos/Delete/5
        public ActionResult Delete(int? id)
        {

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            PRODUTO produto = db.PRODUTO.Find(id);

            if (produto == null)
            {
                return HttpNotFound();
            }

            ProdutoViewModel produtoView = new ProdutoViewModel();

            produtoView.PROD_ID = produto.PROD_ID;
            produtoView.PROD_NOME = produto.PROD_NOME;
            produtoView.PROD_ESTOQUE = produto.PROD_ESTOQUE;
            produtoView.PROD_VALOR = produto.PROD_VALOR;

            return View(produtoView);

        }
        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            PRODUTO produto = db.PRODUTO.Find(id);
            db.PRODUTO.Remove(produto);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: Produtos/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            PRODUTO produto = db.PRODUTO.Find(id);

            if (produto == null)
            {

                return HttpNotFound();

            }

            ProdutoViewModel produtoView = new ProdutoViewModel();

            produtoView.PROD_ID = produto.PROD_ID;
            produtoView.PROD_NOME = produto.PROD_NOME;
            produtoView.PROD_ESTOQUE = produto.PROD_ESTOQUE;
            produtoView.PROD_VALOR = produto.PROD_VALOR;

            return View(produtoView);
        }

        public ActionResult Index(string sortOrder, string searchString)
        {

            var produtos = from x in db.PRODUTO
                           select x;

            if (searchString != null)
                produtos = produtos.Where(x => x.PROD_NOME.Contains(searchString));

            if (sortOrder != null)
                if (sortOrder == "PROD_ID")
                    produtos = produtos.OrderBy(x => x.PROD_ID);
                else 
                    produtos = produtos.OrderBy(x => x.PROD_NOME);


            List<ProdutoViewModel> produtosViews = new List<ProdutoViewModel>();

            foreach (PRODUTO item in produtos)
            {

                ProdutoViewModel temp = new ProdutoViewModel();

                temp.PROD_ID = item.PROD_ID;
                temp.PROD_NOME = item.PROD_NOME;
                temp.PROD_ESTOQUE = item.PROD_ESTOQUE;
                temp.PROD_VALOR = item.PROD_VALOR;

                produtosViews.Add(temp);


            }


            return View(produtosViews);

        }

    }
}