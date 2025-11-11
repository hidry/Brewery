/**
 * Dynamically determines the API URL based on the current window location.
 * This allows the app to work correctly in different environments:
 * - localhost (port 8800 with relative path)
 * - GitHub Codespaces (dynamic URLs with port forwarding)
 * - Production deployments (relative URLs)
 */
function getApiUrl(): string {
  const { protocol, hostname, port } = window.location;

  // If running on localhost
  if (hostname === 'localhost' || hostname === '127.0.0.1') {
    // In production build, we typically serve from the same port as backend
    return '/api';
  }

  // For GitHub Codespaces URLs (pattern: xxx-YYYY.app.github.dev)
  if (hostname.includes('.app.github.dev')) {
    // Extract and replace port number in hostname
    const portMatch = hostname.match(/-(\d+)\.app\.github\.dev$/);
    if (portMatch) {
      const currentPort = portMatch[1];
      // If we're on a different port (like 4200), change to 8800
      if (currentPort !== '8800') {
        const backendHostname = hostname.replace(/-\d+\.app\.github\.dev$/, '-8800.app.github.dev');
        return `${protocol}//${backendHostname}/api`;
      }
      // Already on port 8800, use relative path
      return '/api';
    }
  }

  // For other remote hosts with explicit port numbers
  if (port && port !== '8800') {
    // We're on a different port, explicitly use port 8800
    return `${protocol}//${hostname}:8800/api`;
  }

  // Default: use relative path (works when frontend and backend share the same origin)
  return '/api';
}

export const environment = {
  production: true,
  apiUrl: getApiUrl()
};
