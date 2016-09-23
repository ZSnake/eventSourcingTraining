MSBUILD_PATH = "C:/Windows/Microsoft.NET/Framework/v4.0.30319/msbuild.exe"
MSPEC_PATH = "lib/Machine.Specifications.0.8.3/tools/mspec-clr4.exe"
VERSION = ENV["APPVEYOR_BUILD_VERSION"] || "local"
MSTEST_PATH = File.join(ENV['VS110COMNTOOLS'], '..', 'IDE', 'mstest.exe')
BUILD_PATH = File.expand_path('build')
DATABASE_DEPLOYMENT_PATH = File.expand_path('database_deployment')
DEPLOY_PATH = File.expand_path('deploy')
REPORTS_PATH = File.expand_path('testresults')
TEST_RESULTS_PATH = File.expand_path('testresults')
SOLUTION = "Unicron.sln"
SOLUTION_PATH = File.join("src",SOLUTION)
TRXFILE = File.join(REPORTS_PATH, SOLUTION + '.trx')
CONFIG = "Debug"
PATH_7ZIP = "C:/Program Files/7-Zip/7z.exe"
WEB_APP = "Unicron.Web"

task :default => [:all]

task :all => [:build, :specs, :createArtifact]

task :build => [:removeArtifacts, :compile]

task :createArtifact do
	puts 'Creating deployment artifact...'
	Dir.mkdir(DEPLOY_PATH) unless Dir.exists?(DEPLOY_PATH)	
	Dir.chdir("build/_publishedWebsites/#{WEB_APP}") do
		sh "\"#{PATH_7ZIP}\" a \"#{DEPLOY_PATH}/#{WEB_APP}-#{VERSION}.zip\" *"
	end
end


task :removeArtifacts do
	require 'fileutils'
	FileUtils.rm_rf BUILD_PATH
	FileUtils.rm_rf REPORTS_PATH
	FileUtils.rm_rf DEPLOY_PATH
	FileUtils.rm_rf DATABASE_DEPLOYMENT_PATH
end
	
task :compile do
	puts 'Compiling solution...'
	sh "#{MSBUILD_PATH} /p:Configuration=#{CONFIG} /p:OutDir=\"#{BUILD_PATH}/\" /p:PostBuildEvent=\"\" #{SOLUTION_PATH}"	
end

task :specs do
    puts "Locating and running MSpec tests in #{BUILD_PATH}."
    puts 'This can take a while, please wait...'
    
    # Create output directory if necessary
    mkdir REPORTS_PATH unless File.exist?(REPORTS_PATH)
    
    # Clear out old reports, but don't talk about it.
    verbose(false) do
        FileList.new("#{REPORTS_PATH}/*").each { |file| rm file }
    end
    
    # Find all the specs DLLs
    testDlls = FileList.new("#{BUILD_PATH}/*.Specs.dll")
    
    # Join the file list into a single string with quotes surrounding the file paths
    TEST_DLL_LIST = '"' + testDlls.join('" "') + '"'
    
    # Specify our mspec command line parameters
    HTML_SWITCH = "--html \"#{REPORTS_PATH}\""
    XML_SWITCH = "--xml \"#{REPORTS_PATH}\\testresults.xml\""
    
    # Run the tests and give a message on failure
    # But we don't want to hear the chatter, so dump the console output to a file
    sh "#{MSPEC_PATH} #{HTML_SWITCH} #{XML_SWITCH} #{TEST_DLL_LIST}"
    result = $?.to_i
    
    raise "One or more tests failed.  Please see results in #{REPORTS_PATH}\\index.html" unless result == 0

    # If we get here, all's good
    puts "All tests passed"
end

task :dbRebuild => [:dbDrop, :dbCreate, :dbSeed]

task :dbDrop => [:compile] do
	Dir.chdir("build/") do
		sh "DatabaseDeployer.exe drop"
	end
end

task :dbCreate => [:compile] do
	Dir.chdir("build/") do
		sh "DatabaseDeployer.exe create"
	end
end

task :dbUpdate => [:compile] do
	Dir.chdir("build/") do
		sh "DatabaseDeployer.exe update"
	end
end

task :dbSeed => [:compile] do
	Dir.chdir("build/") do
		sh "DatabaseDeployer.exe seed"
	end
end
