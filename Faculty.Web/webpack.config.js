const path = require("path");
const webpack = require("webpack");
const ExtractTextPlugin = require("extract-text-webpack-plugin");
//const FlowBabelWebpackPlugin = require('flow-babel-webpack-plugin');
const bundleOutputDir = "./wwwroot/dist";

module.exports = (env) => {
    const isDevBuild = env !== "prod";
    return [
        {
            entry: {
                bundle: "./ClientApp/index.js",
            },
            output: {
                path: path.join(__dirname, bundleOutputDir),
                filename: "[name].js",
                publicPath: "dist/"
            },
            module: {
                rules: [
                    {
                        test: /\.(js|jsx)?$/,
                        include: /ClientApp/,
                        exclude: /node_modules/,
                        use: {
                            loader: "babel-loader",
                            options: { 
                                presets: ["es2015", "react", "stage-0"]
                            }
                        }
                    },
                    { test: /\.css$/, use: isDevBuild ? ["style-loader", "css-loader"] : ExtractTextPlugin.extract({ use: "css-loader?minimize" }) },
                    { test: /\.(png|jpg|jpeg|gif|svg|woff|woff2)$/, use: "url-loader?limit=25000" }
                ]
            },
            plugins: [
                /*new webpack.optimize.CommonsChunkPlugin({
                    name: "bundle",
                    minChunks: Infinity
                }),
                new FlowBabelWebpackPlugin()*/
            ].concat(isDevBuild ? [
                new webpack.SourceMapDevToolPlugin({
                    filename: "[file].map", // Remove this line if you prefer inline source maps
                    moduleFilenameTemplate: path.relative(bundleOutputDir, "[resourcePath]") // Point sourcemap entries to the original file locations on disk
                })
            ] : [
                new webpack.optimize.UglifyJsPlugin({
                    comments: false
                }),
                new ExtractTextPlugin("site.css")
            ])
        }
    ];
};