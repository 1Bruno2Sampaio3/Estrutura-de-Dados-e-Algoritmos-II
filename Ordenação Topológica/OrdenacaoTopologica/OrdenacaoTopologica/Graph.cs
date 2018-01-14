using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdenacaoTopologica
{
    public class Graph
    {


        #region Atributos

        /// <summary>
        /// Lista de nós que compõe o grafo.
        /// </summary>
        public List<Node> nodes;
        
        #endregion

        #region Propridades

        /// <summary>
        /// Mostra todos os nós do grafo.
        /// </summary>
        //public Node[] Nodes
        //{
        //    get { return this.nodes.ToArray(); }
        //}

        public List<Node> Nodes
        {
            get { return this.nodes; }
        }

        #endregion

        #region Construtores

        /// <summary>
        /// Cria nova instância do grafo.
        /// </summary>
        public Graph()
        {
            this.nodes = new List<Node>();

        }

        #endregion

        #region Métodos

        /// <summary>
        /// Encontra o nó através do seu nome.
        /// </summary>
        /// <param name="name">O nome do nó.</param>
        /// <returns>O nó encontrado ou nulo caso não encontre nada.</returns>
        public Node FindNode(string name)
        {
            foreach (Node n in nodes)
            {
                if (n.Name == name) return n;
            }
            return null;
        }

        /// <summary>
        /// Adiciona um nó ao grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser adicionado.</param>
        /// <param name="info">A informação a ser armazenada no nó.</param>
        public void AddNode(string name)
        {
            this.AddNode(name, null);
        }

        public void AddNodeCriado(Node n)
        {
            if(FindNode(n.Name) == null)
            nodes.Add(n);
        }

        /// <summary>
        /// Adiciona um nó ao grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser adicionado.</param>
        /// <param name="info">A informação a ser armazenada no nó.</param>
        public void AddNode(string name, object info)
        {
            if (FindNode(name) == null)
                nodes.Add(new Node(name, info));
        }

        /// <summary>
        /// Remove um nó do grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser removido.</param>
        public void RemoveNode(string name)
        {
            Node n = FindNode(name);
            if (n != null) nodes.Remove(n);
        }

        /// <summary>
        /// Adiciona o arco entre dois nós associando determinado custo.
        /// </summary>
        /// <param name="from">O nó de origem.</param>
        /// <param name="to">O nó de destino.</param>
        /// <param name="cost">O cust associado.</param>
        public void AddEdge(string from, string to, int cost)
        {
            Node f = FindNode(from);
            Node t = FindNode(to);
            if (f != null && t != null)
                f.AddEdge(t, cost);
        }

        public void AddEdgeNode(Node n, Node m)
        {
            if (n != null && m != null)
                n.AddEdge(m);
        }

        /// <summary>
        /// Obtem todos os nós vizinhos de determinado nó.
        /// </summary>
        /// <param name="node">O nó origem.</param>
        /// <returns></returns>
        public Node[] GetNeighbours(string from)
        {
            Node f = FindNode(from);
            Node[] res = new Node[f.Edges.Count];
            int i = 0;
            foreach (Edge e in f.Edges)
            {
                res[i++] = e.To;
            }
            return res;
        }

        /// <summary>
        /// Valida um caminho, retornando a lista de nós pelos quais ele passou.
        /// </summary>
        /// <param name="nodes">A lista de nós por onde passou.</param>
        /// <param name="path">O nome de cada nó na ordem que devem ser encontrados.</param>
        /// <returns></returns>
        public bool IsValidPath(ref Node[] nodes, params string[] path)
        {
            nodes = new Node[path.Length];
            int i = 0;
            string origem = "";
            nodes[i++] = FindNode(path[0]);
            foreach (string s in path)
            {
                string destino = s;
                if (origem != "")
                {
                    Node o = FindNode(origem);
                    Node d = FindNode(destino);
                    if (o == null || d == null) return false;
                    if (!GetNeighbours(origem).Contains(d)) return false;
                    nodes[i++] = d;
                }
                origem = s;
            }

            return true;
        }


        public void ClearVisited()
        {
            foreach (Node n in nodes)
            {
                n.Visited = false;
            }
        }

        public List<Node> ShortestPath(string begin, string end)
        {
            Graph q = new Graph();
            Node n = new Node(begin, 0)
            {
                Parent = null
            };
            q.nodes.Add(n);


            while (!n.Name.Equals(end))
            {
                n = null;
                foreach (Node node in q.nodes.ToList())
                {
                    Node externo = this.FindNode(node.Name);
                    foreach (Edge e in externo.Edges)
                    {
                        Node interno = q.FindNode(e.To.Name);
                        if (interno == null)
                        {
                            if (e.To.FOLGA == 0)
                            {
                                n = new Node(e.To.Name, e.To.FOLGA)
                                {
                                    Parent = node
                                };
                                q.nodes.Add(n);
                                q.AddEdge(n.Name, n.Parent.Name, 0);
                            }
                        }
                    }
                }
            }
            return q.nodes;
        }

        public bool ExisteCaminho(string n_origem, string n_destino)
        {
            ClearVisited();
            Node n = Encontra(n_destino, FindNode(n_origem));
            ClearVisited();
            if (n == null) { return false; }
            return true;
        }

        #endregion

        #region Ordenação Topológica

        //Retorna os vétices na forma de Ordenação Topológica
        public List<Node> Ordenar(Graph grafo)
        {
            Stack<Node> CaminhoPilha = new Stack<Node>();
            HashSet<Node> visitado = new HashSet<Node>();

            //Loop para alcançar todos os vérties
            foreach(Node node in grafo.Nodes)
            {
                if(!visitado.Contains(node))
                {
                    BuscaEmProfundidade(node, visitado, CaminhoPilha);
                }
            }

            //Agora desempilho o resultado e adiciono em uma lista
            List<Node> resultado = new List<Node>();

            while(CaminhoPilha.Count > 0)
            {
                resultado.Add(CaminhoPilha.Pop());
            }
                
            //retorna o resultado
            return resultado;

        }
        

        private void BuscaEmProfundidade(Node n, HashSet<Node> visitado, Stack<Node> caminhoPilha)
        {
            visitado.Add(n);

            foreach(Edge edge in n.Edges)
            {
                if (!visitado.Contains(edge.To))
                {
                    BuscaEmProfundidade(edge.To, visitado, caminhoPilha);
                }
            }

            caminhoPilha.Push(n);
        }

        #endregion

        #region EncontraCiclo

        public bool ExisteCiclo()
        {
            bool r = false;
            foreach (Node n in Nodes)
            {
                r = NoCiclico(n);
                if (r) { break; }
            }
            return r;
        }

        private bool NoCiclico(Node n)
        {
            foreach (Edge a in n.Edges)
            {
                if (n == Encontra(n.Name, a.To)) { return true; }
            }
            return false;
        }

        private Node Encontra(string nome, Node n)
        {
            
            if (n == null || n.Name == nome) { return n; }
            n.Visited = true;
            foreach (Edge a in n.Edges)
            {
                if (!a.To.Visited)
                {
                    Node aux = Encontra(nome, a.To);
                    if (aux != null) { return aux; }
                }
            }
            return null;
        }

        #endregion
    }
}
