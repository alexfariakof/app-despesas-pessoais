for i in `find /home/database/ -name "*.sql" | sort --version-sort`; do mysql -uroot -p!12345 DespesasPessoaisDB < $i; done;


