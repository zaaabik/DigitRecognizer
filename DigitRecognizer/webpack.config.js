const path = require('path');
const webpack = require('webpack');
const isProduction = process.env.NODE_ENV === 'production';
const CopyWebpackPlugin = require('copy-webpack-plugin');
const {VueLoaderPlugin} = require('vue-loader');

function cssLoaders(options) {
    options = options || {};

    const cssLoader = {
        loader: 'css-loader',
        options: {
            sourceMap: options.sourceMap
        }
    };

    const postcssLoader = {
        loader: 'postcss-loader',
        options: {
            sourceMap: options.sourceMap
        }
    };

    function generateLoaders(loader, loaderOptions) {
        const loaders = options.usePostCSS ? [cssLoader, postcssLoader] : [cssLoader];
        if (loader) {
            loaders.push({
                loader: loader + '-loader',
                options: Object.assign({}, loaderOptions, {
                    sourceMap: options.sourceMap
                })
            })
        }
    }

    return {
        css: generateLoaders(),
        postcss: generateLoaders(),
        sass: generateLoaders('sass')
    }
}

module.exports =
    {
        entry: {
            index: path.resolve(__dirname, 'wwwroot/src/Scripts/index.js'),
        },
        output: {
            path: path.resolve(__dirname, 'wwwroot/dist'),
            filename: '[name].js',
            publicPath: '/dist/'
        },
        resolve: {
            extensions: ['.js', '.vue', '.json'],
            alias: {
                'vue$': 'vue/dist/vue.esm.js',
                '@': path.resolve(__dirname, 'wwwroot/src')
            }
        },
        module: {
            rules: [
                {
                    test: /Locales/,
                    loader: '@alienfast/i18next-loader',
                    exclude: /(node_modules|bower_components)/,
                    options: {basenameAsNamespace: true}
                },
                {
                    test: /\.vue$/,
                    loader: 'vue-loader',
                    options: {
                        loaders: cssLoaders({
                            sourceMap: !isProduction,
                            usePostCSS: true
                        }),
                        cssSourceMap: !isProduction
                    }
                },
                {
                    test: /\.(ttf|woff|woff2|eot|svg)$/,
                    use: [
                        {
                            loader: 'file-loader',
                            options: {
                                limit: 8192,
                                name: 'fonts/[name].[ext]',
                                publicPath: '/dist/'
                            }
                        }
                    ]
                },
                {
                    test: /\.s[a|c]ss$/,
                    use: [{
                        loader: "style-loader"
                    },
                        {
                            loader: "css-loader"
                        },
                        {
                            loader: "postcss-loader"
                        },
                        {
                            loader: "sass-loader"
                        }]
                },
                {
                    test: /\.css/,
                    use: [{
                        loader: "style-loader"
                    }, {
                        loader: "css-loader"
                    }, {
                        loader: "postcss-loader"
                    }]
                },
                {
                    test: /\.js$/,
                    loader: 'babel-loader',
                    exclude: /(node_modules|bower_components)/
                }
            ]
        },
        devtool: !isProduction ? 'eval-source-map' : false,
        mode: isProduction ? 'production' : 'development',
        plugins: [            
            new VueLoaderPlugin()
        ]
    };
