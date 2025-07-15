FROM node:20-alpine AS builder

WORKDIR /app

COPY package*.json ./
RUN npm ci

COPY . .
RUN npm run build --prod 
RUN ls -la /app/dist/stock-management

FROM nginx:stable-alpine

COPY --from=builder /app/dist/stock-management/browser /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
