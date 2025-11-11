const fs = require('fs');
const path = require('path');

/**
 * Deployment script for copying Angular build output to the shared Web directory.
 * This eliminates the need to manually copy files to multiple directories.
 */

const sourceDir = path.join(__dirname, 'dist', 'brewery');
const targetDir = path.join(__dirname, '..', 'Server', 'Web');

/**
 * Recursively copy directory contents
 * @param {string} src - Source directory
 * @param {string} dest - Destination directory
 */
function copyRecursive(src, dest) {
  // Create destination directory if it doesn't exist
  if (!fs.existsSync(dest)) {
    fs.mkdirSync(dest, { recursive: true });
  }

  // Read source directory
  const entries = fs.readdirSync(src, { withFileTypes: true });

  for (const entry of entries) {
    const srcPath = path.join(src, entry.name);
    const destPath = path.join(dest, entry.name);

    if (entry.isDirectory()) {
      // Recursively copy subdirectories
      copyRecursive(srcPath, destPath);
    } else {
      // Copy file
      fs.copyFileSync(srcPath, destPath);
      console.log(`Copied: ${entry.name}`);
    }
  }
}

/**
 * Clean target directory by removing all files
 * @param {string} dir - Directory to clean
 */
function cleanDirectory(dir) {
  if (!fs.existsSync(dir)) {
    return;
  }

  const entries = fs.readdirSync(dir, { withFileTypes: true });

  for (const entry of entries) {
    const fullPath = path.join(dir, entry.name);

    if (entry.isDirectory()) {
      // Recursively delete subdirectory
      fs.rmSync(fullPath, { recursive: true, force: true });
    } else {
      // Delete file
      fs.unlinkSync(fullPath);
    }
  }
}

// Main deployment process
console.log('=== Brewery WebApp Deployment ===\n');

// Check if source directory exists
if (!fs.existsSync(sourceDir)) {
  console.error(`Error: Build output directory not found: ${sourceDir}`);
  console.error('Please run "npm run build" first.');
  process.exit(1);
}

console.log(`Source: ${sourceDir}`);
console.log(`Target: ${targetDir}\n`);

try {
  // Clean target directory
  console.log('Cleaning target directory...');
  cleanDirectory(targetDir);

  // Copy files
  console.log('Copying files...\n');
  copyRecursive(sourceDir, targetDir);

  console.log('\n✓ Deployment completed successfully!');
  console.log(`Files deployed to: ${targetDir}`);
} catch (error) {
  console.error('Error during deployment:', error.message);
  process.exit(1);
}
