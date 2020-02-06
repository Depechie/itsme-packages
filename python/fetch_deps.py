from os import name as operating_system
from subprocess import call

# Download ITSME libraries
lib_version = '0.5.0.1579093712'
location = './itsme'

fetch_deps_exit_code = None

if operating_system == 'nt':
    fetch_deps_exit_code = call(f'../scripts/dependencies.ps1 -libVersion {lib_version} -location {location}', shell=True)
else:
    fetch_deps_exit_code = call(f'../scripts/dependencies.sh {lib_version} {location}', shell=True)

if fetch_deps_exit_code != 0:
    raise SystemExit("Failed to fetch dependencies")