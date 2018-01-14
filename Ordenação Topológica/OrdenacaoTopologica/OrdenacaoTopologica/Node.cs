using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdenacaoTopologica
{
    public class Node
    {

        #region Propriedades

        /// <summary>
        /// Indica se o nó foi visitado.
        /// </summary>
        public bool Visited { get; set; }
        /// <summary>
        /// O nome do nó dentro do grafo.
        /// </summary>
        public Node Parent { get; set; }
        /// <summary>
        /// O nome do nó dentro do grafo.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// A informação adicional armazenada no nó.
        /// </summary>
        public object Info { get; set; }
        /// <summary>
        /// A lista de arcos associada ao nó.
        /// </summary>
        public List<Edge> Edges { get; private set; }
        /// <summary>
        /// Indica se o nó foi visitado.
        /// </summary>
        public int IC { get; set; }
        /// <summary>
        /// Indica se o nó foi visitado.
        /// </summary>
        public int TC { get; set; }
        /// <summary>
        /// Indica se o nó foi visitado.
        /// </summary>
        public int TT { get; set; }
        /// <summary>
        /// Indica se o nó foi visitado.
        /// </summary>
        public int IT { get; set; }
        /// <summary>
        /// Indica se o nó foi visitado.
        /// </summary>
        public int FOLGA { get; set; }
        /// Indica se o nó foi visitado.
        /// </summary>
        public int TEMPODURACAO { get; set; }
        /// Indica se o nó foi visitado.
        /// </summary>
        /// 
        public int ge;
        public int GRAU_DE_ENTRADA
        {
            get
            {
                return ge;
            }

            set
            {
                ge = value;
            }
        }


        #endregion

        #region Construtores

        /// <summary>
        /// Cria um novo nó.
        /// </summary>
        public Node()
        {
            this.Edges = new List<Edge>();
        }

        /// <summary>
        /// Cria um novo nó.
        /// </summary>
        /// <param name="name">O nome do nó.</param>
        /// <param name="info">A informação armazenada no nó.</param>
        public Node(string name, object info) : this()
        {
            this.Name = name;
            this.Info = info;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Adiciona um arco com nó origem igual ao nó atual, e destino e custo de acordo com os parâmetros.
        /// </summary>
        /// <param name="to">O nó destino.</param>
        public void AddEdge(Node to)
        {
            AddEdge(to, 0);
        }

        /// <summary>
        /// Adiciona um arco com nó origem igual ao nó atual, e destino e custo de acordo com os parâmetros.
        /// </summary>
        /// <param name="to">O nó destino.</param>
        /// <param name="cost">O custo associado ao arco.</param>
        public void AddEdge(Node to, int cost)
        {
            this.Edges.Add(new Edge(this, to, cost));
        }

        /// <summary>
        /// Remove o arco com origem igual ao nó atual 
        /// </summary>
        /// <param name="to"></param>
        public void RemoveEdge(Node to)
        {
            Edge edge = new Edge(this, to, 0);
            foreach (Edge e in Edges)
            {
                if (e.ToString() == edge.ToString())
                {
                    Edges.Remove(e);
                    break;
                }
            }
        }

        #endregion

        #region Métodos Sobrescritos

        /// <summary>
        /// Apresenta a visualização do objeto em formato texto.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this.Info != null)
            {
                return String.Format("{0}({1})", this.Name, this.Info);
            }
            return this.Name;
        }

        #endregion

    }
}
