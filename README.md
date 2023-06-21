<h2 align="center"> 
    :construction:  Projeto em construção  :construction:
</h2>

<h2 align="center"> 
   API REST DESPESAS PESSOAIS 
</h2>

<h2 align="left"> 
Acesso documentação da API no Swagger <a target="_blank" href="http://api-despesas-pessoais-aspnetcore.bwg2czahbvgefufr.eastus.azurecontainer.io/swagger/index.html" target="_parent" >backend do projeto</a>
      <h6 align="justify">
        Obs.: Não é possivél executar através do swagger a maioria dos end-points pois todos estam com autenticação via Bearer Token. Utilizar Workspace Postaman é uma opção depois de realizar cadastro, fazer o singIn que retorna o token para ser utilizado nos headers dos end points.
    </h6>
</h2>

<h2 align="left"> 
Acesso ao protótipo <a href="https://despesas-pessoasis-aws.d26q19cgt5w2n4.amplifyapp.com/" target="_parent" >frontend</a> do projeto
    <h6 align="justify">
        Obs.: Não há necessidade de se cadastrar para acessar ao protótipo frontend pois é apenas uma demo com as principais             funcionalidades         da aplicação funcionando de maneira fake sem conexão a API responsável por fazer persistência dos dados e controle de acesso.
    </h6>
</h2>

<h2 align="left"> 
Acesso a aplicação atualizada em produção  <a href="http://despesas-pessoais-azure.cdeefmd5a6fjfece.eastus.azurecontainer.io" target="_parent" >Despesas Pessoais</a> 
    <h6 align="justify">
    Obs.: Existe um usuário teste já criado para acessar aplicação "login=teste@teste.com/senha=teste"
    </h6>        
</h2>

<h2 align="left"> 
Workspace para teste da API Público  <a href="https://www.postman.com/bold-eclipse-872793/workspace/api-despesas-pessoais-azure" target="_blank" >Postman</a>
</h2>



# Descrição 
<h5> 
   <p>API Rest HATEOAS  dockerizada Despesas Pessoas usando .Net core 6 com banco de dados mysql server usando Entity Framework e Migrations. O objetivo é atualizar da versão .net core 2.2 para .net 6 adicioando novas funcinalidades como TTD e CI/CD na  implantação do projeto
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
      <li>
         <h6>Camada Model View da API</h6>
            <p>A Camada repositorio está idependente e não pode ser vista sua implementação pois o modelo   exposto nos endpoint são Objetos modelView que não espõem a implementação do banco.
         </p>
      </li>
      <li>
         <h6>Acesso a API</h6>
            <p> Apenas Primeiro Acesso no caso cadastro de um novo usuário, Login do usuário e recuperação de senhas podem ser acessados sem a necessidade de realizar login com usuário e senhas validadas. O restante da API só pode ser acessada através de um accessToken gerado pela API. 
         </p>
      </li>
      <li>
         <h6>Arquivos de Configuração </h6>
            <p> Arquivos contendo chaves de configuração com chave de cripitografia,  acesso ao banco de dados e amazon S3 estão protegidos pois não se encontram expostos no repositório GitHub assim como a imagem dockerizada está configurada como private evitando assim acesso aos arquivos de configuração. 
         </p>
      </li>
      <li>
         <h6>Controle de Usuários </h6>
            <p> Apenas usuários com perfil de administrador podem listar os usuários e efetuar a deleção de usuários. Por questões de segurança das informações dos usuários cadastrados todos os acessos ao Endpoint usuários estão com Auntenticação JWT Bearer Ativadas.
         </p>
      </li>       
      <li>
         <h6>Acesso ao Banco de Dados</h6>
            <p> Configurações de acesso ao banco de dados não se encontram mais no arquivo appsettings.json estam separados em um arquivo chamado MYSQL_ConnectionString.txt. Foi necessário pois o coódigo fonte esta estposto aqui no github e  aplicação se encontra dockerizada em uma imagem privada do dokcer hub e em produção no azure. Com isso é evitado acesso indevido a string de conexão de acesso ao banco de dados.    </p>
      </li>             
 </ul>
</h5>
