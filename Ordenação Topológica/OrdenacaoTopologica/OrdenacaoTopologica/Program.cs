using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdenacaoTopologica
{
    public class Program
    {
        public static Graph aux;
        public static Node inicio, fim;

        static void Main(string[] args)
        {
            #region MAIN
            int op;
            do
            {
                Console.WriteLine("1 - Criar meu Grafo e no fim executar a ORDENAÇÃO TOPOLOGICA\n2 - Encontrar o CAMINHO CRITICO em um projeto");
                op = Convert.ToInt32(Console.ReadLine());
            } while (op < 1 || op > 2);
            

            switch (op)
            {
                case 1: PegarInformacoes(); break;
                case 2: Projeto(); break;
            }


            #endregion

        }

        public static void PegarInformacoes()
        {
            #region Recebe_Informações
            Graph g = new Graph();
            aux = g;
            var elementos = new HashSet<Node>();
            Node n;
            string name1, origem, destino;

            int op, op2, qtd;

            do
            {
                Console.WriteLine("Quantidades de nos que serão criados: ");
                qtd = Convert.ToInt32(Console.ReadLine());

                for (int i = 0; i < qtd; i++)
                {
                    do
                    {

                        Console.WriteLine("Digite o nome do Nó {0}: ", i+1);
                        name1 = Console.ReadLine();
                        g.AddNode(name1);
                        n = g.FindNode(name1);
                    } while (elementos.Contains(n));

                    elementos.Add(n);
                }

                g = new Graph();
                int eleCount = elementos.Count;
                for(int j = 0; j < eleCount; j++)
                {
                    n = elementos.First();
                    g.AddNode(n.Name);
                    elementos.Remove(n);
                }

                Console.WriteLine("Estabelecer Ligacoes? (Edge(s) - Arco(s)) 1 - Sim / 2 - Nao");
                op2 = Convert.ToInt32(Console.ReadLine());
                if (op2 == 1)
                {
                    do
                    {
                        do
                        {
                            Console.WriteLine("Digite o nó ORIGEM: ");
                            origem = Console.ReadLine();
                        } while (g.FindNode(origem) == null);

                        do
                        {
                            Console.WriteLine("Digite o nó DESTINO: ");
                            destino = Console.ReadLine();
                        } while (g.FindNode(destino) == null);

                        g.AddEdge(origem, destino, 0);

                        Console.WriteLine("Adicionar novo Arco? 1 - Sim / 2 - Não");
                        op = Convert.ToInt32(Console.ReadLine());
                    } while (op == 1);
                    op2 = op;
                }
                
            } while (op2 != 2);

            if (!(g.ExisteCiclo()))
            {
                MostrarOrdenacaoTopologica(g);
            }
            else
            {
                Console.WriteLine("Não é possível fazer a ordenação topológica desse grafo pois contém CICLO!");
            }

            Console.ReadLine();

        #endregion
        }

        public static void MostrarOrdenacaoTopologica(Graph g)
        {
            #region Ordena_O_Grafo
            List<Node> ln = g.Ordenar(g);
            Console.Write("\n\n");
            Console.WriteLine("Imprimindo uma das possíveis ordenações topológicas do digrafo criado");
            Console.Write("\n\n");
            foreach (Node cadaN in ln)
            {
                Console.WriteLine(cadaN.Name);
            }

            Console.ReadLine();

            #endregion
        }

        public static void Projeto()
        {
            #region Recebe_Informações
            Graph g = new Graph();
            aux = g;
            var elementos = new HashSet<Node>();
            Node n;
            string name1, origem, destino;

            int op, op2, qtd;

            do
            {
                Console.WriteLine("Quantidades de nos que serão criados: ");
                qtd = Convert.ToInt32(Console.ReadLine());

                for (int i = 0; i < qtd; i++)
                {
                    do
                    {

                        Console.WriteLine("Digite o nome do Nó {0}: ", i + 1);
                        name1 = Console.ReadLine();
                        g.AddNode(name1);
                        n = g.FindNode(name1);
                    } while (elementos.Contains(n));
                    
                    Console.Write("Digite a duração: \t");
                    Console.WriteLine();
                    n.TEMPODURACAO = Convert.ToInt32(Console.ReadLine());

                    elementos.Add(n);
                }

                g = new Graph();
                int eleCount = elementos.Count;
                for (int j = 0; j < eleCount; j++)
                {
                    n = elementos.First();
                    g.AddNodeCriado(n);
                    elementos.Remove(n);
                }

                Console.WriteLine("Estabelecer Ligacoes? (Edge(s) - Arco(s)) 1 - Sim / 2 - Nao");
                op2 = Convert.ToInt32(Console.ReadLine());
                if (op2 == 1)
                {
                    do
                    {
                        do
                        {
                            Console.WriteLine("Digite o nó ORIGEM: ");
                            origem = Console.ReadLine();
                        } while (g.FindNode(origem) == null);
                        Node m = g.FindNode(origem);
                        do
                        {
                            Console.WriteLine("Digite o nó DESTINO: ");
                            destino = Console.ReadLine();
                        } while (g.FindNode(destino) == null);
                        Node l = g.FindNode(destino);
                        //Calculo o grau de entrada de quandos arcos chegam no nó, Será muito útil isso
                        l.GRAU_DE_ENTRADA++;
                        g.AddEdgeNode(m, l);

                        Console.WriteLine("Adicionar novo Arco? 1 - Sim / 2 - Não");
                        op = Convert.ToInt32(Console.ReadLine());
                    } while (op == 1);
                    op2 = op;
                }

                //Guardo a referência de quem eu chego no nó
                foreach(Node y in g.Nodes)
                {
                    foreach(Edge u in y.Edges)
                    {
                        u.To.Parent = y;
                    }
                }
                
                if(!g.ExisteCiclo())
                {
                    op2 = CalculaValores(g);
                    if (op2 == 1)
                    {
                        Console.WriteLine("Deseja inserir um novo os dados novamente desde o inicio para um novo grafo? 1 - Sim / 2 - Não");
                        op2 = Convert.ToInt32(Console.ReadLine());
                        if (op2 == 1)
                        {
                            g = new Graph();
                            elementos = new HashSet<Node>();
                            Console.WriteLine();
                        }

                    }
                    else
                    {
                        op2 = 2;
                    }
                }
                else
                {
                    Console.WriteLine("IMPOSSOVEL calcular o CAMINHO CRITICO do grafo criado!");
                    Console.WriteLine("Deseja inserir um novo os dados novamente desde o inicio para um novo grafo? 1 - Sim / 2 - Não");
                    op2 = Convert.ToInt32(Console.ReadLine());
                    if(op2 == 1)
                    {
                        g = new Graph();
                        elementos = new HashSet<Node>();
                        Console.WriteLine();
                    }
                }
               

            } while (op2 != 2);

           
            Console.ReadLine();

            #endregion
        }

        public static int CalculaValores(Graph g)
        {
            #region Calcula_Valore_Projeto
            //List<Node> ln = g.Ordenar(g);
            //ln.Reverse();

            int quantidade_de_graus_de_entrada = 0;

            foreach(Node p in g.Nodes)
            {
                if(p.GRAU_DE_ENTRADA == 0)
                {
                    quantidade_de_graus_de_entrada++;
                }
            }

            #region IF_VERIFICA_GRAUS_DE_ENTRADAS

            if(quantidade_de_graus_de_entrada == 1)
            {
                foreach (Node y in g.Nodes)
                {
                    if (y.Parent == null && y.GRAU_DE_ENTRADA == 0)
                    {
                        y.IC = 0;
                        inicio = y;
                        y.TC = y.IC + y.TEMPODURACAO;
                    }

                    else
                    {
                        if (y.GRAU_DE_ENTRADA == 1)
                        {
                            y.IC = y.Parent.TC;
                            y.TC = y.IC + y.TEMPODURACAO;
                        }
                        else
                        {
                            string name = y.Name;
                            int maior = 0;
                            foreach (Node h in g.Nodes)
                            {
                                foreach (Edge f in h.Edges)
                                {
                                    if (f.To.Name.Equals(name))
                                    {
                                        if (maior < h.TC)
                                        {
                                            maior = h.TC;
                                        }
                                    }
                                }
                            }

                            y.IC = maior;
                            y.TC = y.IC + y.TEMPODURACAO;
                        }
                    }
                }

                List<Node> ln = g.Ordenar(g);
                ln.Reverse();

                foreach (Node r in ln)
                {
                    if (r.Edges.Count == 0)
                    {
                        r.TT = r.TC;
                        r.IT = r.TT - r.TEMPODURACAO;
                        fim = r;
                    }
                    else
                    {
                        if (r.Edges.Count == 1)
                        {
                            r.TT = r.Edges[0].To.IT;
                            r.IT = r.TT - r.TEMPODURACAO;

                        }
                        else
                        {
                            int menor = int.MaxValue;

                            foreach (Edge e in r.Edges)
                            {
                                if (e.To.IT < menor)
                                {
                                    menor = e.To.IT;
                                }
                            }
                            r.TT = menor;
                            r.IT = r.TT - r.TEMPODURACAO;
                        }
                    }
                }

            

                CalculaFolga(g);
                EncontraCaminhoCritico(g);

                return 0;
                #endregion
            }
            else
            {
                Console.WriteLine("O projeto proposto tem muitas taredas sem antecessoras. Não tem como estabelecer o CAMINHO CRITICO do projeto!");
                return 1;
            }



            #endregion
        }

        public static void CalculaFolga(Graph g)
        {
            #region Calcula_Folga_Tarefas_Do_Projeto

            foreach(Node f in g.Nodes)
            {
                g.FindNode(f.Name).FOLGA = f.TT - f.TC;
            }

            #endregion
        }

        public static void EncontraCaminhoCritico(Graph g)
        {
            #region Encontra_Caminho_Crítico_Com_Djikstra

            List<Node> caminhoCritico = g.ShortestPath(inicio.Name, fim.Name);

            Console.Write("\n\n");
            foreach(Node n in caminhoCritico)
            {
                Console.WriteLine(n.Name);
            }

            #endregion
        }

    }
}
