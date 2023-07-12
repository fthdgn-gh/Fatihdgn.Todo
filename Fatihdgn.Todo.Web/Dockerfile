# Base image
FROM node:18-alpine

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

# Expose the port used by the Angular application (change it if necessary)
EXPOSE 4200

# Command to start the Angular application
CMD ["npm", "run", "start", "--", "--host", "0.0.0.0", "--port", "4200"]