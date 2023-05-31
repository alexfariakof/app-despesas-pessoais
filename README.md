<h2 align="center"> 
    :construction:  Projeto em construção  :construction:
</h2>

<h2 align="center"> 
   API REST DESPESAS PESSOAIS 
</h2>

<h2 align="left"> 
# Acesso ao protótipo <a href="http://api-despesas-pessoais-aspnetcore.bwg2czahbvgefufr.eastus.azurecontainer.io/swagger/index.html" target="_blank" >backend</a> de projeto
</h2>

<h2 align="left"> 
# Workspace para teste da API Público  <a href="https://www.postman.com/bold-eclipse-872793/workspace/api-despesas-pessoais-azure" target="_blank" >Postman</a>
</h2>

# Descrição 
<h5> 
   <p>API Rest dockerizada Despesas Pessoas usando .Net core 6 e TTD, banco de dados em memória. O objetivo é atualizir da versão .net core 2.2 para .net 6 adicioando novas funcinalidades como TTD e CI/CD na  implantação do projeto
   </p>
</h5>

# Configurações de Segurança
<h5> 
   <ul>
      <li>   
         <h6>Controle de Acesso/Senha de Usuários</h6>
            <p>As Senhas estão cripitografadas e não são expostas em nenhuma requisição e por uma chave que não esta exposta no repositorio acessada pelo classe Crypto em um arquivo criado dentro do servidor. O ideal seria a acessar uma chave crida dentro do própio Azure ou AWS criando assim mais segurança.
         </p>
      </li>
      </li> 
         <h6>Camada Model View da API</h6>
            <p>A Camada repositorio está idependente e não pode ser vista sua implementação pois o modelo   exposto nos endpoint são Objetos modelView que não espõem a implementação do banco.
         </p>
      </li>
      <li>
         <h6>Acesso a API</h6>
            <p> Apenas Primeiro Acesso no caso cadastro de um novo usuário, Login do usuário e recuperação de senhas podem ser acessados sem a necessidade de realizar login com usuário e senhas validadas. O restante da API só pode ser acessada através de um accessToken gerado pela API. 
         </p>
      </li>
      </ul>
</h5>
