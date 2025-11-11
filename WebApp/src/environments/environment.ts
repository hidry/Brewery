// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

/**
 * Dynamically determines the API URL based on the current window location.
 * This allows the app to work correctly in different environments:
 * - localhost development (port 4200 -> port 8800)
 * - GitHub Codespaces (dynamic URLs with port forwarding)
 * - Production (relative URLs)
 */
function getApiUrl(): string {
  const { protocol, hostname, port } = window.location;

  // If running on localhost
  if (hostname === 'localhost' || hostname === '127.0.0.1') {
    // If frontend is on port 4200 (ng serve), backend is on 8800
    if (port === '4200') {
      return 'http://localhost:8800/api';
    }
    // If frontend is on port 8800 or no specific port, use relative path
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
  production: false,
  apiUrl: getApiUrl()
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
