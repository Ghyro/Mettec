ENV_FILE=".env"

create_env() {
cat > "$ENV_FILE" <<EOL
# Database
POSTGRES_USER=postgres
POSTGRES_PASSWORD=postgres
POSTGRES_DB=mettec-service-api-database

# pgAdmin
PGADMIN_DEFAULT_EMAIL=admin@gmail.com
PGADMIN_DEFAULT_PASSWORD=admin

EOL
echo ".env file created with default values."
}

if [ -f "$ENV_FILE" ]; then
    echo ".env file exists. Loading values..."
else
    echo ".env file not found. Creating default..."
    create_env
fi

echo "Using .env with values:"
cat "$ENV_FILE"

# Export to .env
export $(grep -v '^#' $ENV_FILE | xargs)

echo "Starting docker compose..."
docker compose up --build