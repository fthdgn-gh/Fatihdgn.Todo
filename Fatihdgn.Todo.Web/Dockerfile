# Base image
FROM node:18-alpine as builder

# Set the working directory inside the container
WORKDIR /app

# Copy package.json and package-lock.json to the working directory
COPY package*.json ./

# Install project dependencies
RUN npm install

# Copy the rest of the application code to the working directory
COPY . .

# Build the Angular project
RUN npm run build

FROM nginx:1.25.1-alpine

COPY --from=builder /app/dist/fatihdgn.todo.web /usr/share/nginx/html

# Remove default Nginx configurations
RUN rm /etc/nginx/conf.d/default.conf

# Copy custom Nginx configuration
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Expose the port used by the Angular application (change it if necessary)
EXPOSE 80

# Start Nginx server
CMD ["nginx", "-g", "daemon off;"]