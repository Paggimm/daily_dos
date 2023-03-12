/* eslint-env node */
require('@rushstack/eslint-patch/modern-module-resolution')

module.exports = {
  root: true,
  'extends': [
    'plugin:vue/vue3-essential',
    'eslint:recommended',
    '@vue/eslint-config-typescript',
    '@vue/eslint-config-prettier/skip-formatting',
    "plugin:vue/base",
    "plugin:vue/vue3-strongly-recommended",
    "plugin:vue/vue3-recommended",
    "plugin:@typescript-eslint/eslint-recommended",
    "plugin:@typescript-eslint/recommended",
    "@vue/typescript",
    "@vue/typescript/recommended"
  ],
  parserOptions: {
    ecmaVersion: 'latest'
  },
  settings: {
    'import/resolver': {
      node: {
        extensions: ['.js', '.vue', '.ts', '.d.ts'],
      },
      alias: {
        extensions: ['.vue', '.js', '.ts', '.scss', '.d.ts'],
        map: [
            ['@', './src'],
          ],
      },
    },
  },
  plugins: [
    "no-null"
  ],
  rules: {
    "no-null/no-null": "error",
    "prefer-const": "warn",
    "vue/singleline-html-element-content-newline": "off",
    "vue/multi-word-component-names": "off",
    "indent": [
      "warn",
      4
    ],
    "vue/html-indent": [
      "warn",
      4
    ]
  }
}
