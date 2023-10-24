
# Run your SQL commands to grant privileges and flush privileges.
mysql -uroot -ppassword <<EOF
ALTER USER 'root'@'localhost' IDENTIFIED BY '!12345';
GRANT ALL PRIVILEGES ON *.* TO 'root'@'localhost' IDENTIFIED BY 'password' WITH GRANT OPTION;
FLUSH PRIVILEGES;
EOF

service mysql restart


for i in `find /home/database/ -name "*.sql" | sort --version-sort`; do mysql -uroot -p!12345 DespesasPessoaisDB < $i; done;


