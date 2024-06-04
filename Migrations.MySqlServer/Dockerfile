FROM mysql:8.0
EXPOSE 3306
COPY ./init_database.sh /docker-entrypoint-initdb.d/init_database.sh
RUN chmod +x /docker-entrypoint-initdb.d/init_database.sh