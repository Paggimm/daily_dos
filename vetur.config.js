/** @type {import('vls').VeturConfig} */
module.exports = {
    projects: [
        {
            root: './vue_client', //root of subproject
            package: './package.json', // It is relative to root property.
            tsconfig: './tsconfig.json',  // It is relative to root property.
        }
    ]
}
