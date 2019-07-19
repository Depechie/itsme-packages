from setuptools import find_packages, setup

setup(
    name='itsme',
    version='0.0.4',
    author='itsme_sdk',
    author_email='itsme.store@inthepocket.mobi',
    description='A package to integrate with the itsme OIDC API',
    long_description='This is a library to integrate with the ITSME backend',
    long_description_content_type='text/markdown',
    url='https://github.com/itsme-api/itsme-api-python',
    packages=find_packages(),
    classifiers=[
        'Programming Language :: Python :: 3',
        'License :: OSI Approved :: MIT License',
        'Operating System :: OS Independent',
    ],
    include_package_data=True
)
